using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Repositories.InMemory
{
    public class InMemoryDataStore : IEnumerable<KeyValuePair<Type, IList<object>>>
    {
        private readonly Dictionary<Type, IList<object>> internalStore;
        private bool useTransactionDataStore;

        public InMemoryDataStore()
        {
            this.internalStore = new Dictionary<Type, IList<object>>();
        }

        public InMemoryTransaction Transaction
        {
            get;
            set;
        }

        public InMemoryTransaction BeginTransaction()
        {
            this.Transaction = new InMemoryTransaction(this);
            this.Transaction.Begin();

            return this.Transaction;
        }

        public IDisposable CurrentTransactionDataStore()
        {
            if (this.Transaction == null)
            {
                throw new InvalidOperationException("No transaction exists.");
            }

            if (this.useTransactionDataStore)
            {
                return null;
            }

            this.useTransactionDataStore = true;
            
            return new CurrentTransactionDataStoreDisposable(this);
        }

        public void Save(object entity)
        {
            if (this.useTransactionDataStore)
            {
                this.Transaction.CurrentTransactionDataStore.Save(entity);
                return;
            }

            this.SaveToInternalStore(entity);
        }

        private void SaveToInternalStore(object entity)
        {
            var type = entity.GetType();

            if (!this.internalStore.ContainsKey(type))
            {
                this.internalStore.Add(type, new List<object>());
            }

            var list = this.internalStore[type];
            if (!list.Contains(entity))
            {
                list.Add(entity);
            }
            else
            {
                var index = list.IndexOf(entity);
                list[index] = entity;
            }
        }

        public void Remove<T>(T entity)
        {
            if (this.useTransactionDataStore)
            {
                this.Transaction.CurrentTransactionDataStore.Remove(entity);
                return;
            }

            var type = entity.GetType();

            if (this.internalStore.ContainsKey(type))
            {
                var list = this.internalStore[type];
                list.Remove(entity);
            }
        }

        public bool Contains<T>(T entity)
        {
            return this.Contains(entity, EqualityComparer<T>.Default);
        }

        public bool Contains<T>(T entity, IEqualityComparer<T> comparer)
        {
            if (this.useTransactionDataStore)
            {
                return this.Transaction.CurrentTransactionDataStore.Contains(entity, comparer);
            }

            var type = entity.GetType();

            if (this.internalStore.ContainsKey(type))
            {
                var list = this.internalStore[type];
                return list.Cast<T>().Contains(entity, comparer);
            }

            return false;
        }

        public IEnumerable<T> List<T>()
        {
            if (this.useTransactionDataStore)
            {
                return this.Transaction.CurrentTransactionDataStore.List<T>();
            }

            var type = typeof(T);

            if (this.internalStore.ContainsKey(type))
            {
                return this.internalStore[type].Cast<T>();
            }
            else
            {
                return Enumerable.Empty<T>();
            }
        }

        public void Clear()
        {
            if (this.useTransactionDataStore)
            {
                this.Transaction.CurrentTransactionDataStore.Clear();
                return;
            }

            this.internalStore.Clear();
        }

        internal void Merge(InMemoryDataStore dataStore)
        {
            foreach (var store in dataStore.internalStore)
            {
                foreach (var d in store.Value)
                {
                    this.SaveToInternalStore(d);
                }
            }
        }

        public IEnumerator<KeyValuePair<Type, IList<object>>> GetEnumerator()
        {
            if (this.useTransactionDataStore)
            {
                return this.Transaction.CurrentTransactionDataStore.GetEnumerator();
            }

            return this.internalStore.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class CurrentTransactionDataStoreDisposable : IDisposable
        {
            private readonly InMemoryDataStore inMemoryDataStore;

            public CurrentTransactionDataStoreDisposable(InMemoryDataStore inMemoryDataStore)
            {
                this.inMemoryDataStore = inMemoryDataStore;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                inMemoryDataStore.useTransactionDataStore = false;
            }
        }
    }
}

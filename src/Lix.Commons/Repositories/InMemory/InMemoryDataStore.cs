using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Repositories.InMemory
{
    public class InMemoryDataStore : IEnumerable<KeyValuePair<Type, IList<object>>>
    {
        private readonly Dictionary<Type, IList<object>> internalStore;

        public InMemoryDataStore()
        {
            this.internalStore = new Dictionary<Type, IList<object>>();
        }

        public InMemoryTransaction Transaction
        {
            get;
            set;
        }

        private bool IsTransactionActive
        {
            get
            {
                return this.Transaction != null;
            }
        }

        public InMemoryTransaction BeginTransaction()
        {
            this.Transaction = new InMemoryTransaction(this);
            this.Transaction.Begin();

            return this.Transaction;
        }

        public void Save(object entity)
        {
            if (this.IsTransactionActive)
            {
                this.Transaction.CurrentTransactionDataStore.Save(entity);
                return;
            }

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
            if (this.IsTransactionActive)
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
            if (this.IsTransactionActive)
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
            if (this.IsTransactionActive)
            {
                return this.Transaction.CurrentTransactionDataStore.List<T>();
            }

            var type = typeof (T);

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
            if (this.IsTransactionActive)
            {
                this.Transaction.CurrentTransactionDataStore.Clear();
                return;
            }

            this.internalStore.Clear();
        }

        internal void Merge(InMemoryDataStore dataStore)
        {
            foreach (var store in dataStore)
            {
                foreach (var d in store.Value)
                {
                    this.Save(d);
                }
            }
        }

        public IEnumerator<KeyValuePair<Type, IList<object>>> GetEnumerator()
        {
            if (this.IsTransactionActive)
            {
                return this.Transaction.CurrentTransactionDataStore.GetEnumerator();
            }

            return this.internalStore.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

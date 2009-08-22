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

        public void Save(object entity)
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
            var type = entity.GetType();

            if (this.internalStore.ContainsKey(type))
            {
                var list = this.internalStore[type];
                list.Remove(entity);
            }
        }

        public IEnumerable<T> List<T>()
        {
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
            this.internalStore.Clear();
        }

        public void Merge(InMemoryDataStore dataStore)
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
            return this.internalStore.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

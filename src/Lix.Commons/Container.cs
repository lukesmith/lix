using System;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons
{
    public class Container : IDisposable
    {
        private readonly IDictionary<Type, IList<Type>> innerContainer;

        public Container()
        {
            this.innerContainer = new Dictionary<Type, IList<Type>>();
        }

        public void Register(Type type)
        {
            if (!this.innerContainer.ContainsKey(type))
            {
                this.innerContainer.Add(type, new List<Type>());
            }

            if (!this.innerContainer[type].Contains(type))
            {
                this.innerContainer[type].Add(type);
            }
        }

        public void RegisterForType(Type forType, Type type)
        {
            if (!this.innerContainer.ContainsKey(forType))
            {
                this.innerContainer.Add(forType, new List<Type>());
            }

            if (!this.innerContainer[forType].Contains(type))
            {
                this.innerContainer[forType].Add(type);
            }
        }

        public Type FindTypeFor<T>()
        {
            return this.FindTypeFor(typeof (T));
        }

        public Type FindTypeFor(Type type)
        {
            if (this.innerContainer.ContainsKey(type))
            {
                return this.innerContainer[type].LastOrDefault();
            }

            return null;
        }

        public Type FindTypeFor(Func<Type, bool> predicate)
        {
            var key = this.innerContainer.Keys.LastOrDefault(predicate);

            return this.FindTypeFor(key);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            this.innerContainer.Clear();
        }
    }
}
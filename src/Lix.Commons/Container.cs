using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Lix.Commons
{
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
        Justification = "Reviewed. Suppression is OK here."),
     Obsolete("Change so third party container has to be used.")]
    public class Container : IDisposable
    {
        private readonly IDictionary<Type, IList<Type>> innerContainer;

        public Container()
        {
            this.innerContainer = new Dictionary<Type, IList<Type>>();
        }

        public void Register(Type type)
        {
            this.RegisterForType(type, type);
        }

        public void RegisterForType(Type forType, Type type)
        {
            if (forType == null)
            {
                throw new ArgumentNullException("forType");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

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
            return this.FindTypeFor(typeof(T));
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
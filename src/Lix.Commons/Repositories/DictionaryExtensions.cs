using System;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Repositories
{
    internal static class DictionaryExtensions
    {
        public static bool ContainsKeyImplementing<T>(this IDictionary<Type, IList<object>> dictionary)
        {
            return dictionary.Keys.Any(typeof(T).IsAssignableFrom);
        }

        public static IEnumerable<T> GetInstancesImplementing<T>(this IDictionary<Type, IList<object>> dictionary)
        {
            var keys = dictionary.Keys.Where(typeof(T).IsAssignableFrom);

            return dictionary.Where(x => keys.Contains(x.Key)).SelectMany(x => x.Value).Cast<T>();
        }
    }
}
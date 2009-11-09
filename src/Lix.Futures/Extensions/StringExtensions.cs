using System;

namespace Lix.Futures.Extensions
{
    public static class StringExtensions
    {
        public static bool Like(this string source, string compareTo, ComparisonType comparisonType)
        {
            switch (comparisonType)
            {
                case ComparisonType.Contains:
                    return source.Contains(compareTo);
                case ComparisonType.EndsWith:
                    return source.EndsWith(compareTo);
                case ComparisonType.StartsWith:
                    return source.StartsWith(compareTo);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
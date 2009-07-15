using System;
using MbUnit.Framework;

namespace Lix.Commons.Tests
{
    public static class UnitTestingExtensions
    {
        public static FluentAnd<T> ShouldSatisfy<T>(this T testTarget, Predicate<T> predicate)
        {
            Assert.IsTrue(predicate.Invoke(testTarget));
            return new FluentAnd<T>(testTarget);
        }

        public static FluentAnd<T> ShouldBeEqualTo<T>(this T testTarget, T comparisonObject)
        {
            Assert.AreEqual(comparisonObject, testTarget);
            return new FluentAnd<T>(testTarget);
        }

        public static FluentAnd<T> ShouldBeTheSameObjectAs<T>(this T testTarget, Object comparisonObject)
        {
            Assert.AreEqual(comparisonObject, testTarget);
            return new FluentAnd<T>(testTarget);
        }
    }
}
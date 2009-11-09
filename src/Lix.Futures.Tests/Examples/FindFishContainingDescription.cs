using System;
using System.Linq.Expressions;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using Lix.Futures.Extensions;

namespace Lix.Futures.Tests.Examples
{
    public class FindFishContainingDescription : DefaultQueryableSpecification<Fish>
    {
        private readonly string description;

        public FindFishContainingDescription(string description)
        {
            this.description = description;
        }

        protected override Expression<Func<Fish, bool>> Predicate
        {
            get
            {
                return x => x.Description.Like(this.description, ComparisonType.Contains);
            }
        }
    }
}
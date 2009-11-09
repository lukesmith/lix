using System;
using System.Linq.Expressions;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class FindFishWithDescriptionSpecification : DefaultQueryableSpecification<Fish>
    {
        private readonly string description;

        public FindFishWithDescriptionSpecification(string description)
        {
            this.description = description;
        }

        protected override Expression<Func<Fish, bool>> Predicate
        {
            get
            {
                return x => x.Description == this.description;
            }
        }
    }
}
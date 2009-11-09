using System;
using System.Linq.Expressions;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class FindFishWithIdSpecification : DefaultQueryableSpecification<Fish>
    {
        private readonly int id;

        public FindFishWithIdSpecification(int id)
        {
            this.id = id;
        }

        protected override Expression<Func<Fish, bool>> Predicate
        {
            get
            {
                return x => x.Id == this.id;
            }
        }
    }
}
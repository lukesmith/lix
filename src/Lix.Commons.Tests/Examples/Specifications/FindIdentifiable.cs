using System;
using System.Linq.Expressions;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class FindIdentifiable : DefaultQueryableSpecification<IIdentifiable>
    {
        private readonly int id;

        public FindIdentifiable(int id)
        {
            this.id = id;
        }

        protected override Expression<Func<IIdentifiable, bool>> Predicate
        {
            get
            {
                return x => x.Id == this.id;
            }
        }
    }
}
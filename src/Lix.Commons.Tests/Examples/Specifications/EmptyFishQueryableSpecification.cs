using System;
using System.Linq.Expressions;
using Lix.Commons.Specifications;

namespace Lix.Commons.Tests.Examples.Specifications
{
    public class EmptyFishQueryableSpecification : DefaultQueryableSpecification<Fish>
    {
        protected override Expression<Func<Fish, bool>> Predicate
        {
            get
            {
                return x => true;
            }
        }
    }
}
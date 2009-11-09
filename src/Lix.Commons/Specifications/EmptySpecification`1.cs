using System;
using System.Linq.Expressions;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents an empty specification.
    /// </summary>
    /// <typeparam name="TEntity">Type type of the entity to build the specification for.</typeparam>
    public class EmptySpecification<TEntity> : DefaultQueryableSpecification<TEntity>
    {
        protected override Expression<Func<TEntity, bool>> Predicate
        {
            get
            {
                return x => true;
            }
        }
    }
}
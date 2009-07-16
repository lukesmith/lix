using System.Linq;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a <see cref="IQueryable{TEntity}"/> <see cref="ISpecification"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to build a specification for.</typeparam>
    public interface IQueryableSpecification<TEntity> : ISpecification<IQueryable<TEntity>, IQueryable<TEntity>>
    {
    }
}

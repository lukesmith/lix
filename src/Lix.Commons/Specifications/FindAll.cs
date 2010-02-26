using System.Linq;

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification that finds all <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type type of the entity to build the specification for.</typeparam>
    public class FindAll<TEntity> : DefaultQueryableSpecification<TEntity>
        where TEntity : class
    {
        protected override IQueryable<TEntity> Build(IQueryable<TEntity> context)
        {
            return context;
        }
    }
}
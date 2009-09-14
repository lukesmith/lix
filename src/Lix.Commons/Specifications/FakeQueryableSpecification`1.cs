using System.Linq;

namespace Lix.Commons.Specifications
{
    internal class FakeQueryableSpecification<TEntity> : IQueryableSpecification<TEntity>
        where TEntity : class
    {
        private readonly IQueryable<TEntity> data;

        public FakeQueryableSpecification(IQueryable<TEntity> data)
        {
            this.data = data;
        }

        public object Build(object context)
        {
            return this.Build(context as IQueryable<TEntity>);
        }

        /// <summary>
        /// Builds the specification.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public IQueryable<TEntity> Build(IQueryable<TEntity> context)
        {
            return this.data;
        }
    }
}
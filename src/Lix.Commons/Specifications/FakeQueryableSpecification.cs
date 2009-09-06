using System.Linq;

namespace Lix.Commons.Specifications
{
    internal class FakeQueryableSpecification : IQueryableSpecification<object>
    {
        private readonly IQueryable data;

        public FakeQueryableSpecification(IQueryable data)
        {
            this.data = data;
        }

        public object Build(object context)
        {
            return this.Build(context as IQueryable<object>);
        }

        /// <summary>
        /// Builds the specification.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// An object representing the built specification.
        /// </returns>
        public IQueryable<object> Build(IQueryable<object> context)
        {
            return this.data.Cast<object>();
        }
    }
}
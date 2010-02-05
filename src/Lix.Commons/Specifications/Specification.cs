namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    public class Specification
    {
        private static SpecificationInterceptors interceptors;

        public static ISpecificationInterceptor Intercept<TSpecification>()
            where TSpecification : ISpecification
        {
            var interceptor = LixObjectFactory.CreateSpecificationInterceptor();

            Interceptors.Add<TSpecification>(interceptor);

            return interceptor;
        }

        public static SpecificationInterceptors Interceptors
        {
            get
            {
                if (interceptors == null)
                {
                    interceptors = new SpecificationInterceptors();
                }

                return interceptors;
            }
        }

        /// <summary>
        /// Creates an <see cref="FindAll{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to create the specification for.</typeparam>
        /// <returns>
        /// An <see cref="FindAll{TEntity}"/> object.
        /// </returns>
        public static IQueryableSpecification<TEntity> Empty<TEntity>()
        {
            return new FindAll<TEntity>();
        }
    }
}

namespace Lix.Commons.Specifications
{
    /// <summary>
    /// Represents a specification.
    /// </summary>
    public class Specification
    {
        /// <summary>
        /// Creates an <see cref="EmptySpecification{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to create the specification for.</typeparam>
        /// <returns>
        /// An <see cref="EmptySpecification{TEntity}"/> object.
        /// </returns>
        public static IQueryableSpecification<TEntity> Empty<TEntity>()
        {
            return new FindAll<TEntity>();
        }

        public static ISpecificationInterceptor Intercept<TSpecification>()
            where TSpecification : ISpecification
        {
            var interceptor = LixObjectFactory.CreateSpecificationInterceptor();

            Interceptors.Add<TSpecification>(interceptor);

            return interceptor;
        }

        private static SpecificationInterceptors _interceptors;
        public static SpecificationInterceptors Interceptors
        {
            get
            {
                if (_interceptors == null)
                {
                    _interceptors = new SpecificationInterceptors();
                }

                return _interceptors;
            }
        }
    }
}

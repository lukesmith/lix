namespace Lix.Commons.Specifications
{
    internal class DefaultSpecificationExecutorInitializer : ISpecificationExecutorInitializer
    {
        public DefaultSpecificationExecutorInitializer()
        {
            SpecificationExecutorFactory.RegisterSpecificationExecutor(typeof(IQueryableSpecification<>), typeof(QueryableSpecificationExecutor<>));
        }
    }
}
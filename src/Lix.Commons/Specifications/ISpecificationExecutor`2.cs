namespace Lix.Commons.Specifications
{
    public interface ISpecificationExecutor<TSpecification, TEntity> : ISpecificationExecutor<TEntity>
        where TSpecification : ISpecification
        where TEntity : class
    {
        TSpecification Specification
        {
            get;
        }
    }
}
namespace Lix.Commons.Specifications
{
    public interface ISpecificationExecutor<TSpecification, TEntity> : ISpecificationExecutor<TEntity>
    {
        new TSpecification Specification
        {
            get;
        }
    }
}
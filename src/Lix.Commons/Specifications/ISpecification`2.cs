namespace Lix.Commons.Specifications
{
    public interface ISpecification
    {
    }

    public interface ISpecification<TContext, TResult> : ISpecification
    {
        TResult Build(TContext context);
    }
}

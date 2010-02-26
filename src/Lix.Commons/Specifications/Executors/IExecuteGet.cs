namespace Lix.Commons.Specifications.Executors
{
    public interface IExecuteGet<TEntity>
        where TEntity : class
    {
        TEntity Get();
    }
}
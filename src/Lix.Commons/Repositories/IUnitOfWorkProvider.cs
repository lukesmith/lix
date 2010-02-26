namespace Lix.Commons.Repositories
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Current { get; }
    }
}
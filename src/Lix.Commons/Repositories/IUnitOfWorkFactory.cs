namespace Lix.Commons.Repositories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
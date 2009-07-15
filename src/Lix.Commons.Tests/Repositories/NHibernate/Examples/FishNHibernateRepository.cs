using Lix.Commons.Repositories.NHibernate;

namespace Lix.Commons.Tests.Repositories.NHibernate.Examples
{
    public class FishNHibernateRepository : NHibernateRepositoryBase<Fish>
    {
        public FishNHibernateRepository(NHibernateUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
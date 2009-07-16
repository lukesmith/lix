using Lix.Commons.Repositories.NHibernate;
using Lix.Commons.Tests.Examples;

namespace Lix.Commons.Tests.Repositories.NHibernate.Examples
{
    public class FishNHibernateRepository : NHibernateRepositoryBase<Fish>
    {
        public FishNHibernateRepository(NHibernateUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
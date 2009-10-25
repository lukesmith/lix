using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;

namespace Lix.NHibernate.Utilities.Tests.Repositories.Examples
{
    public class FishNHibernateRepository : NHibernateRepositoryBase<Fish>
    {
        public FishNHibernateRepository(NHibernateUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
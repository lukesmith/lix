using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;

namespace Lix.Futures.Tests.NHibernate
{
    public class FutureFishNHibernateRepository : NHibernateRepositoryBase<Fish>
    {
        public FutureFishNHibernateRepository(NHibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override IQueryable<Fish> RepositoryQuery
        {
            get
            {
                return LixNHibernateExtensions.Linq<Fish>(this.UnitOfWork.Session);
            }
        }
    }
}
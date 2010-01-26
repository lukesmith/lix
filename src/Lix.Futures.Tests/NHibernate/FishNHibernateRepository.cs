using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications.Executors;
using Lix.Commons.Tests.Examples;

namespace Lix.Futures.Tests.NHibernate
{
    public class FutureFishNHibernateRepository : NHibernateRepository<Fish>
    {
        public FutureFishNHibernateRepository(NHibernateUnitOfWork unitOfWork, ISpecificationExecutorFactory specificationExecutorFactory)
            : base(unitOfWork, specificationExecutorFactory)
        {
        }

        protected override IQueryable<Fish> GetRepositoryQuery()
        {
            return LixNHibernateExtensions.Linq<Fish>(this.UnitOfWork.Session);
        }
    }
}
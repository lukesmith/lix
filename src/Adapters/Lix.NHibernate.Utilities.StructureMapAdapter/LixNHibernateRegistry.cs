using Lix.Commons.Repositories;
using StructureMap.Configuration.DSL;

namespace Lix.NHibernate.Utilities.StructureMapAdapter
{
    public class LixNHibernateRegistry : Registry
    {
        public LixNHibernateRegistry()
        {
            var queryInstance = this.For(typeof(IReportingRepository<>)).Use(typeof(NHibernateRepository<>));
            this.For(typeof(ILinqEnabledRepository<>)).Use(queryInstance);
            this.For(typeof(IDomainRepository<>)).Use(queryInstance);
            this.For(typeof(INHibernateRepository<>)).Use(queryInstance);

            this.For<IUnitOfWork>().Use<NHibernateUnitOfWork>();
        }
    }
}

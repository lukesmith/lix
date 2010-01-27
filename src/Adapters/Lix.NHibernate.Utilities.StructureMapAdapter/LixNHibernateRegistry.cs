using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Lix.NHibernate.Utilities.StructureMapAdapter
{
    public class LixNHibernateRegistry : Registry
    {
        public LixNHibernateRegistry()
        {
            var queryInstance = this.For(typeof(IQueryRepository<>)).Use(typeof(NHibernateRepository<>));
            this.For(typeof(ICommandRepository<>)).Use(typeof(NHibernateRepository<>));
            this.For(typeof(INHibernateRepository<>)).Use(queryInstance);

            this.For(typeof(IUnitOfWork)).LifecycleIs(InstanceScope.Singleton).Use(typeof(NHibernateUnitOfWork));

            this.For(typeof(INHibernateCriteriaSpecification<>)).Use(typeof(DefaultCriteriaFindAll<>));
        }
    }
}

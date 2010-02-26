using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Lix.Commons.Repositories;
using Lix.NHibernate.Utilities.StructureMapAdapter;
using Lix.StructureMapAdapter;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using StructureMap;

namespace Lix.Examples.Repository
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory.Container.Configure(OnConfigureNHibernate);
            //ObjectFactory.Container.Configure(OnConfigureInMemory);

            ObjectFactory.AssertConfigurationIsValid();

            var application = new TheApplication();
            application.Run();

            Console.ReadLine();
        }

        private static void OnConfigureNHibernate(ConfigurationExpression cfg)
        {
            HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();

            var configuration = Fluently.Configure()
                .Mappings(x => x.AutoMappings.Add(AutoMap.AssemblyOf<Person>().Where(t => typeof(Person).IsAssignableFrom(t))))
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql());
            var sessionFactory = configuration.BuildSessionFactory();

            // Configure nhibernate
            cfg.For<ISessionFactory>().Singleton().Use(sessionFactory);
            cfg.For<ISession>().Singleton().Use(x =>
                                                    {
                                                        var session = x.GetInstance<ISessionFactory>().OpenSession();
                                                        session.FlushMode = FlushMode.Commit;

                                                        using (var tx =session.BeginTransaction())
                                                        {
                                                            new SchemaExport(configuration.BuildConfiguration()).Execute(true, true, false, session.Connection, null);
                                                            tx.Commit();
                                                        }

                                                        return session;
                                                    });

            // Configure our IoC container for a nhibernaterepository
            cfg.AddRegistry(new LixNHibernateRegistry());

            // include the LixRegistry
            cfg.AddRegistry(new LixRegistry());

            cfg.Scan(s =>
                         {
                             s.TheCallingAssembly();

                             s.WithDefaultConventions();
                         });
        }

        private static void OnConfigureInMemory(ConfigurationExpression cfg)
        {
            // Configure a single inmemorydatastore to be used
            cfg.For(typeof(InMemoryDataStore)).LifecycleIs(InstanceScope.Hybrid);

            // Configure our IoC container for a inmemoryrepository
            var repositoryInstance = cfg.For(typeof(IReportingRepository<>)).Use(typeof(InMemoryRepository<>));
            cfg.For(typeof(IDomainRepository<>)).Use(repositoryInstance);
            cfg.For(typeof(ILinqEnabledRepository<>)).Use(repositoryInstance);

            cfg.For(typeof(IUnitOfWork)).LifecycleIs(InstanceScope.Hybrid).Use(typeof(InMemoryUnitOfWork));

            // include the LixRegistry
            cfg.IncludeRegistry(new LixRegistry());

            cfg.Scan(s =>
            {
                s.TheCallingAssembly();

                s.WithDefaultConventions();
            });
        }
    }
}

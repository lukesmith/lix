using System;
using System.Collections.Generic;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
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
            //ObjectFactory.Container.Configure(OnConfigureNHibernate);
            ObjectFactory.Container.Configure(OnConfigureInMemory);

            ObjectFactory.AssertConfigurationIsValid();

            RunTheApplication();

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

                             // Register the queryable specification executor convention
                             s.With(new QueryableSpecificationExecutorRegistrationConvention());

                             // Register the nhibernate specification executor convention
                             s.With(new NHibernateCriteriaSpecificationExecutorRegistrationConvention());
                             
                             s.WithDefaultConventions();
                         });
        }

        private static void OnConfigureInMemory(ConfigurationExpression cfg)
        {
            // Configure a single inmemorydatastore to be used
            cfg.For(typeof(InMemoryDataStore)).LifecycleIs(InstanceScope.Singleton);

            // Configure our IoC container for a inmemoryrepository
            cfg.For(typeof(IQueryRepository<>)).Use(typeof(InMemoryRepository<>));
            cfg.For(typeof(ICommandRepository<>)).Use(typeof(InMemoryRepository<>));

            cfg.For(typeof(IUnitOfWork)).LifecycleIs(InstanceScope.Singleton).Use(typeof(InMemoryUnitOfWork));

            // include the LixRegistry
            cfg.IncludeRegistry(new LixRegistry());

            cfg.Scan(s =>
            {
                s.TheCallingAssembly();

                // Register the queryable specification executor convention
                s.With(new QueryableSpecificationExecutorRegistrationConvention());

                s.WithDefaultConventions();
            });
        }

        private static void RunTheApplication()
        {
            using (new UnitOfWorkScope())
            {
                // Add some people to the repository
                var commandRepository = ObjectFactory.GetInstance<ICommandRepository<Person>>();
                commandRepository.Add(new Person { Age = 2, Name = "John" });
                commandRepository.Add(new Person { Age = 5, Name = "Simon" });
                commandRepository.Add(new Person { Age = 65, Name = "Sandie" });
                commandRepository.Add(new Person { Age = 88, Name = "Simon" });
                commandRepository.Add(new Person { Age = 23, Name = "Joan" });
                commandRepository.Add(new Person { Age = 3, Name = "Mary" });
                commandRepository.Add(new Person { Age = 33, Name = "Simon" });
            }

            using (new UnitOfWorkScope())
            {
                // Get the query repository
                var personRepository = ObjectFactory.GetInstance<IQueryRepository<Person>>();

                // Find everyone
                var people = personRepository.List(new FindAll<Person>());
                WritePeople("Everyone", people);

                // Find people with duplicate names
                people = personRepository.List(new FindPeopleWithDuplicateNames());
                WritePeople("People with duplicate names", people);
            }
        }

        private static void WritePeople(string title, IEnumerable<Person> people)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine("========");
            foreach (var person in people)
            {
                Console.WriteLine(person.Name + " " + person.Age);
            }
        }
    }
}

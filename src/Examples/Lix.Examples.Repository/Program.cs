using System;
using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using StructureMap;

namespace Lix.Examples.Repository
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Configure();

            ObjectFactory.Container.Configure(x =>
                                                  {
                                                      // Configure a a single inmemorydatastore to be used
                                                      x.For(typeof(InMemoryDataStore))
                                                          .LifecycleIs(InstanceScope.Singleton);

                                                      // Configure our IoC container for a inmemoryrepository
                                                      x.For(typeof(IQueryRepository<>)).Use(typeof(InMemoryRepository<>));
                                                      x.For(typeof(ICommandRepository<>)).Use(typeof(InMemoryRepository<>));
                                                  });

            ObjectFactory.AssertConfigurationIsValid();

            RunTheApplication();

            Console.ReadLine();
        }

        private static void RunTheApplication()
        {
            // Add some people to the repository
            var commandRepository = ObjectFactory.GetInstance<ICommandRepository<Person>>();
            commandRepository.UnitOfWork.Begin();
            commandRepository.Save(new Person {Id = 2, Name = "John"});
            commandRepository.Save(new Person { Id = 5, Name = "Simon" });
            commandRepository.Save(new Person { Id = 65, Name = "Sandie" });
            commandRepository.Save(new Person { Id = 23, Name = "Joan" });
            commandRepository.Save(new Person { Id = 3, Name = "Mary" });
            commandRepository.Save(new Person { Id = 33, Name = "Simon" });
            commandRepository.UnitOfWork.Commit();

            // Get the query repository
            var personRepository = ObjectFactory.GetInstance<IQueryRepository<Person>>();
            personRepository.UnitOfWork.Begin();
            
            // Find everyone
            var people = personRepository.List(new FindAll<Person>());
            WritePeople("Everyone", people);

            people = personRepository.List(new FindPeopleWithDuplicateNames());
            WritePeople("People with duplicate names", people);
            personRepository.UnitOfWork.Commit();
        }

        private static void WritePeople(string title, IEnumerable<Person> people)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine("========");
            foreach (var person in people)
            {
                Console.WriteLine(person.Name + " " + person.Id);
            }
        }
    }
}

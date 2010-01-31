using System;
using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.StructureMapAdapter;
using StructureMap;

namespace Lix.Examples.Repository
{
    public class TheApplication
    {
        public void Run()
        {
            using (new UnitOfWorkScope())
            {
                // Add some people to the repository
                var commandRepository = ObjectFactory.GetInstance<IDomainRepository<Person>>();
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
                var personRepository = ObjectFactory.GetInstance<IReportingRepository<Person>>();

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
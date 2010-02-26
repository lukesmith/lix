using Lix.Commands;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using StructureMap.Configuration.DSL;

namespace Lix.StructureMapAdapter
{
    public class LixRegistry : Registry
    {
        public LixRegistry()
        {
            this.Scan(s =>
                          {
                              s.AssemblyContainingType<ICommand>();
                              s.WithDefaultConventions();
                          });

            this.For<ICommandPublisherContainer>().Use<CommandPublisherContainer>();
            this.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();

            // Register default IQueryableSpecification<> instance
            this.For(typeof(IQueryableSpecification<>)).Use(typeof(FindAll<>));
        }
    }
}
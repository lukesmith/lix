using System.Collections.Generic;
using Lix.Commands;
using StructureMap;

namespace Lix.StructureMapAdapter
{
    public class CommandPublisherContainer : ICommandPublisherContainer
    {
        private readonly IContainer container;

        public CommandPublisherContainer(IContainer container)
        {
            this.container = container;
        }

        public IEnumerable<ICommandHandler<TCommand>> GetInstances<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = this.container.ForGenericType(typeof(ICommandHandler<>))
                .WithParameters(command.GetType())
                .GetInstanceAs<ICommandHandler<TCommand>>();

            return new List<ICommandHandler<TCommand>> { commandHandler };
        }
    }
}

using System;
using System.Collections.Generic;
using Lix.Commands;
using StructureMap;

namespace Lix.StructureMapAdapter
{
    public class CommandHandlerContainer : ICommandHandlerContainer, IDisposable
    {
        private readonly IContainer container;

        public CommandHandlerContainer(IContainer container)
        {
            this.container = container.GetNestedContainer();
        }

        public IEnumerable<ICommandHandler<TCommand>> GetInstances<TCommand>(TCommand command) where TCommand : ICommand
        {
            this.container.Configure(x => x.For<ICurrentCommandProvider>().Use(new CurrentCommandProvider(command)));

            var commandHandler = this.container.ForGenericType(typeof(ICommandHandler<>))
                .WithParameters(command.GetType())
                .GetInstanceAs<ICommandHandler<TCommand>>();

            return new List<ICommandHandler<TCommand>> { commandHandler };
        }

        public void Dispose()
        {
            this.container.Dispose();
        }
    }

    public interface ICurrentCommandProvider
    {
        ICommand CurrentCommand { get; }
    }

    public class CurrentCommandProvider : ICurrentCommandProvider
    {
        public CurrentCommandProvider(ICommand command)
        {
            this.CurrentCommand = command;
        }

        public ICommand CurrentCommand
        {
            get;
            private set;
        }
    }
}

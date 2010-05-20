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

        public IEnumerable<ICommandHandler<TCommand>> GetInstances<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (typeof(ICommand) == typeof(TCommand))
            {
                throw new ArgumentException("Generic argument TCommand cannot be of type ICommand.");
            }

            var commandHandler = this.container.ForGenericType(typeof(ICommandHandler<>))
                .WithParameters(command.GetType())
                .GetInstanceAs<ICommandHandler<TCommand>>();

            return new List<ICommandHandler<TCommand>> { commandHandler };
        }

        public IEnumerable<ICommandHandler> GetInstances(Type commandType)
        {
            if (!typeof(ICommand).IsAssignableFrom(commandType))
            {
                throw new ArgumentException("commandType is not assignable from ICommand", "commandType");
            }

            var openGenericType = typeof(ICommandHandler<>);
            var closedGenericType = openGenericType.MakeGenericType(commandType);
            var commandHandler = this.container.GetInstance(closedGenericType) as ICommandHandler;

            //var commandHandler2 = this.container.ForGenericType(typeof(ICommandHandler<>))
            //    .WithParameters(commandType)
            //    .GetInstanceAs<ICommandHandler>();

            return new List<ICommandHandler> { commandHandler };
        }

        public void Dispose()
        {
            this.container.Dispose();
        }
    }
}

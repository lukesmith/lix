using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;

namespace Lix.Commands
{
    public class CommandPublisher : ICommandPublisher
    {
        private readonly ICommandPublisherContainer container;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly ICommandLogger commandLogger;

        public CommandPublisher(ICommandPublisherContainer container, IUnitOfWorkFactory unitOfWorkFactory, ICommandLogger commandLogger)
        {
            this.container = container;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.commandLogger = commandLogger;
        }

        public void Publish<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (!command.IsValid)
            {
                var ex = new InvalidCommandException(command, command.Validate());
                this.commandLogger.LogFailure(command, ex);
                throw ex;
            }

            IEnumerable<ICommandHandler<TCommand>> commandHandlers;
            try
            {
                commandHandlers = this.container.GetInstances(command);
            }
            catch (Exception ex)
            {
                this.commandLogger.LogFailure(command, ex);
                throw;
            }

            if (commandHandlers.Count() > 1)
            {
                var ex =
                    new CommandPublishingException(string.Format("Multiple command handlers found for command {0}.", command.GetType()));
                this.commandLogger.LogFailure(command, ex);
                throw ex;
            }

            if (commandHandlers.Count() == 0)
            {
                var ex =
                    new CommandPublishingException(string.Format("No command handlers found for command {0}.", command.GetType()));
                this.commandLogger.LogFailure(command, ex);
                throw ex;
            }

            try
            {
                this.ExecuteInUnitOfWork(() => commandHandlers.Single().Execute(command));
            }
            catch (Exception ex)
            {
                this.commandLogger.LogFailure(command, ex);

                throw;
            }

            this.commandLogger.LogSuccess(command);
        }

        private void ExecuteInUnitOfWork(Action action)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                unitOfWork.Begin();
                action();
                unitOfWork.Commit();
            }
        }
    }
}
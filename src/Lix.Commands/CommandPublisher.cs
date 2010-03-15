using System;
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
                throw new InvalidCommandException(command, command.Validate());
            }

            var commandHandlers = this.container.GetInstances(command);

            if (commandHandlers.Count() > 1)
            {
                throw new CommandPublishingException(string.Format("Multiple command handlers found for command {0}.", command.GetType()));
            }

            if (commandHandlers.Count() == 0)
            {
                throw new CommandPublishingException(string.Format("No command handlers found for command {0}.", command.GetType()));
            }

            try
            {
                using (var unitOfWork = this.unitOfWorkFactory.Create())
                {
                    unitOfWork.Begin();
                    commandHandlers.First().Execute(command);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                this.commandLogger.LogFailure(command, ex);
                throw;
            }

            this.commandLogger.LogSuccess(command);
        }
    }
}
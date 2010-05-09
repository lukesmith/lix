using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;

namespace Lix.Commands
{
    public class CommandPublisher : ICommandPublisher
    {
        private readonly ICommandHandlerContainer commandHandlerContainer;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly ICommandLogger commandLogger;
        private readonly IEventPublisher eventPublisher;

        public CommandPublisher(
            ICommandHandlerContainer commandHandlerContainer,
            IUnitOfWorkFactory unitOfWorkFactory,
            ICommandLogger commandLogger,
            IEventPublisher eventPublisher)
        {
            this.commandHandlerContainer = commandHandlerContainer;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.commandLogger = commandLogger;
            this.eventPublisher = eventPublisher;
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

            var commandHandler = this.GetCommandHandler(command);
            var delayedEventPublisher = new DelayedEventPublisher(this.eventPublisher);
            commandHandler.SetEventPublisher(delayedEventPublisher);

            try
            {
                this.ExecuteInUnitOfWork(() =>
                                             {
                                                 commandHandler.Execute(command);

                                                 // Log the success as part of this unit of work incase it logging success fails.
                                                 this.commandLogger.LogSuccess(command);
                                             });
            }
            catch (Exception ex)
            {
                this.commandLogger.LogFailure(command, ex);

                throw;
            }

            delayedEventPublisher.PublishAllEvents();
        }

        private void ExecuteInUnitOfWork(Action action)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                unitOfWork.Begin();
                try
                {
                    action();
                    unitOfWork.Commit();
                }
                catch
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        private ICommandHandler<TCommand> GetCommandHandler<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            IEnumerable<ICommandHandler<TCommand>> commandHandlers;
            try
            {
                commandHandlers = this.commandHandlerContainer.GetInstances(command);
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

            return commandHandlers.Single();
        }
    }
}
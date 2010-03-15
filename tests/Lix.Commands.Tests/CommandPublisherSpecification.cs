using System.Collections.Generic;
using Machine.Specifications;
using Moq;

namespace Lix.Commands.Tests
{
    public abstract class CommandPublisherSpecification<TCommand>
        where TCommand : ICommand, new()
    {
        private Establish context = () =>
                                        {
                                            Command = new TCommand();
                                            var commandPublisherContainer = new Mock<ICommandPublisherContainer>();
                                            CommandHandler = new Mock<ICommandHandler<TCommand>>();
                                            CommandLogger = new Mock<ICommandLogger>();
                                            commandPublisherContainer
                                                .Setup(x => x.GetInstances(Moq.It.IsAny<TCommand>()))
                                                .Returns(new List<ICommandHandler<TCommand>> { CommandHandler.Object });

                                            CommandPublisher = new CommandPublisher(commandPublisherContainer.Object, new UnitOfWorkFactory(), CommandLogger.Object);
                                        };

        protected static TCommand Command;
        protected static CommandPublisher CommandPublisher;
        protected static Mock<ICommandHandler<TCommand>> CommandHandler;
        protected static Mock<ICommandLogger> CommandLogger;
    }
}
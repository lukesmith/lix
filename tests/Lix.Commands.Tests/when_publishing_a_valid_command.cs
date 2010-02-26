using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher))]
    public class when_publishing_a_valid_command
    {
        private static ValidCommand command;
        private static CommandPublisher commandPublisher;
        private static Mock<ICommandHandler<ValidCommand>> commandHandler;

        private Establish context = () =>
                                        {
                                            command = new ValidCommand();
                                            var commandPublisherContainer = new Mock<ICommandPublisherContainer>();
                                            commandHandler = new Mock<ICommandHandler<ValidCommand>>();
                                            commandPublisherContainer
                                                .Setup(x => x.GetInstances(Moq.It.IsAny<ValidCommand>()))
                                                .Returns(new List<ICommandHandler<ValidCommand>> { commandHandler.Object });

                                            commandPublisher = new CommandPublisher(commandPublisherContainer.Object, new UnitOfWorkFactory());
                                        };

        private Because of = () => commandPublisher.Publish(command);

        private It should_call_the_command_handler = () => commandHandler.Verify(x => x.Execute(command));
        
        public class ValidCommand : Command
        {
        }
    }
}
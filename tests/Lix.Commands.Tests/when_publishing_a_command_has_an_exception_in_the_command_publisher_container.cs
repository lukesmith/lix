using System;
using Machine.Specifications;
using Moq;

namespace Lix.Commands.Tests
{
    public class when_publishing_a_command_has_an_exception_in_the_command_publisher_container
    {
        private Establish context = () =>
                                        {
                                            Command = new ValidCommand();
                                            CommandLogger = new Mock<ICommandLogger>();
                                            commandPublisher = new CommandPublisher(new CommandPublisherContainerStub(), new UnitOfWorkFactory(), CommandLogger.Object);
                                        };

        private Because of = () => ExceptionThrownFromPublishing = Catch.Exception(() => commandPublisher.Publish(Command));

#pragma warning disable 169
        protected Behaves_like<FailedCommandLoggerBehavior> it_logs_a_failed_command;
#pragma warning restore 169

        protected static ICommand Command;
        protected static Mock<ICommandLogger> CommandLogger;
        protected static Exception ExceptionThrownFromPublishing;
        private static CommandPublisher commandPublisher;
    }
}
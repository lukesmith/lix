using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    public class when_no_command_handlers_exist
    {
        private Establish context = () =>
                                        {
                                            Command = new ValidCommand();
                                            CommandLogger = new Mock<ICommandLogger>();
                                            commandPublisher = new CommandPublisher(new CommandPublisherContainerStub(), new UnitOfWorkFactory(), CommandLogger.Object);
                                        };

        private Because of =
            () => ExceptionThrownFromPublishing = Catch.Exception(() => commandPublisher.Publish(Command));

        private It should_cause_an_exception = () => ExceptionThrownFromPublishing.ShouldNotBeNull();

        private It should_cause_an_invalid_operation_exception = () => ExceptionThrownFromPublishing.ShouldBeOfType<CommandPublishingException>();

#pragma warning disable 169
        protected Behaves_like<FailedCommandLoggerBehavior> it_logs_a_failed_command;
#pragma warning restore 169

        protected static ValidCommand Command;
        protected static Exception ExceptionThrownFromPublishing;
        protected static Mock<ICommandLogger> CommandLogger;
        private static CommandPublisher commandPublisher;

        public class ValidCommand : Command
        {
        }

        private class CommandPublisherContainerStub : ICommandPublisherContainer
        {
            public IEnumerable<ICommandHandler<TCommand>> GetInstances<TCommand>(TCommand command)
                where TCommand : ICommand
            {
                return new List<ICommandHandler<TCommand>>();
            }
        }
    }
}
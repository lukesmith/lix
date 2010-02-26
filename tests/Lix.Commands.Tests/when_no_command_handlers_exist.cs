using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Lix.Commands.Tests
{
    public class when_no_command_handlers_exist
    {
        private static ValidCommand command;
        private static CommandPublisher commandPublisher;
        private static Exception exception;

        private Establish context = () =>
                                        {
                                            command = new ValidCommand();
                                            commandPublisher = new CommandPublisher(new CommandPublisherContainerStub(), new UnitOfWorkFactory());
                                        };

        private Because of = () =>
                                 {
                                     try
                                     {
                                         commandPublisher.Publish(command);
                                     }
                                     catch (Exception ex)
                                     {
                                         exception = ex;
                                     }
                                 };

        private It should_cause_an_exception = () => exception.ShouldNotBeNull();

        private It should_cause_an_invalid_operation_exception = () => exception.ShouldBeOfType<CommandPublishingException>();

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
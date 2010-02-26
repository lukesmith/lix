using System;
using System.Collections.Generic;
using Machine.Specifications;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher))]
    public class when_publishing_an_invalid_command
    {
        private static Command command;
        private static Exception exception;

        private Establish context = () =>
                                        {
                                            command = new InvalidCommand();
                                        };

        private Because of = () =>
                                 {
                                     try
                                     {
                                         new CommandPublisher(null, new UnitOfWorkFactory()).Publish(command);
                                     }
                                     catch (Exception ex)
                                     {
                                         exception = ex;
                                     }
                                 };

        private It should_cause_an_exception = () => exception.ShouldNotBeNull();

        private It should_be_an_invalid_command_exception = () => exception.ShouldBeOfType<InvalidCommandException>();

        private class InvalidCommand : Command
        {
            public override IEnumerable<CommandValidationRule> Validate()
            {
                yield return new CommandValidationRule("Invalid");
            }
        }
    }
}

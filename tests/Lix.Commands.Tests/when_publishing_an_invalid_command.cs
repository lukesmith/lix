using System;
using System.Collections.Generic;
using Machine.Specifications;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher))]
    public class when_publishing_an_invalid_command : CommandPublisherSpecification<when_publishing_an_invalid_command.InvalidCommand>
    {
        private Because of = () => exception = Catch.Exception(() => CommandPublisher.Publish(Command));

        private It should_cause_an_exception = () => exception.ShouldNotBeNull();

        private It should_be_an_invalid_command_exception = () => exception.ShouldBeOfType<InvalidCommandException>();

        private static Exception exception;

        public class InvalidCommand : Command
        {
            public override IEnumerable<CommandValidationRule> Validate()
            {
                yield return new CommandValidationRule("Invalid");
            }
        }
    }
}

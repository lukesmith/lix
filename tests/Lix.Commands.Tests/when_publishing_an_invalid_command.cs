using System;
using System.Collections.Generic;
using Machine.Specifications;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher))]
    public class when_publishing_an_invalid_command : CommandPublisherSpecification<when_publishing_an_invalid_command.InvalidCommand>
    {
        private Because of = () => ExceptionThrownFromPublishing = Catch.Exception(() => CommandPublisher.Publish(Command));

        private It should_cause_an_exception = () => ExceptionThrownFromPublishing.ShouldNotBeNull();

        private It should_be_an_invalid_command_exception = () => ExceptionThrownFromPublishing.ShouldBeOfType<InvalidCommandException>();

#pragma warning disable 169
        private Behaves_like<FailedCommandLoggerBehavior> it_logs_a_failed_command;
#pragma warning restore 169

        protected static Exception ExceptionThrownFromPublishing;

        public class InvalidCommand : Command
        {
            public override IEnumerable<CommandValidationRule> Validate()
            {
                yield return new CommandValidationRule("Invalid");
            }
        }
    }
}

using System;
using Machine.Specifications;

namespace Lix.Commands.Tests
{
    [Subject(typeof(CommandPublisher), "Publish")]
    public class when_publishing_a_command_that_fails : CommandPublisherSpecification<ValidCommand>
    {
        private Establish context =
            () => CommandHandler.Setup(x => x.Execute(Moq.It.IsAny<ValidCommand>())).Throws(new Exception());

        private Because of = () => ExceptionThrownFromPublishing = Catch.Exception(() => CommandPublisher.Publish(Command));

#pragma warning disable 169
        private Behaves_like<FailedCommandLoggerBehavior> it_logs_a_failed_command;
#pragma warning restore 169

        protected static Exception ExceptionThrownFromPublishing;
    }
}
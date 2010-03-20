using System;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    [Behaviors]
    public class FailedCommandLoggerBehavior
    {
        private It should_log_a_failure_notification = () => CommandLogger.Verify(x => x.LogFailure(
                                                                                           Moq.It.Is<ICommand>(c => c == Command),
                                                                                           Moq.It.Is<Exception>(e => e == ExceptionThrownFromPublishing)));

        protected static ICommand Command;
        protected static Mock<ICommandLogger> CommandLogger;
        protected static Exception ExceptionThrownFromPublishing;
    }
}
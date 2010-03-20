using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace Lix.Commands.Tests
{
    [Behaviors]
    public class SuccessfulCommandLoggerBehavior
    {
        private It should_log_a_succeeded_notification = () => CommandLogger.Verify(
                                                                   x => x.LogSuccess(Moq.It.Is<ICommand>(c => c == Command)));

        protected static ICommand Command;
        protected static Mock<ICommandLogger> CommandLogger;
    }
}
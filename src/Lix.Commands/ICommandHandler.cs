namespace Lix.Commands
{
    public interface ICommandHandler<in TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        void Execute(TCommand command);
    }

    public interface ICommandHandler
    {
        void Execute(ICommand command);

        void SetEventPublisher(IEventPublisher eventPublisher);
    }
}
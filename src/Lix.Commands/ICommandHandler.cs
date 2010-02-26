namespace Lix.Commands
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
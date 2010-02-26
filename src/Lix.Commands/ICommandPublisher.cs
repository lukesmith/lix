namespace Lix.Commands
{
    public interface ICommandPublisher
    {
        void Publish<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
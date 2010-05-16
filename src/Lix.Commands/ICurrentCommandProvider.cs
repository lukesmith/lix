namespace Lix.Commands
{
    public interface ICurrentCommandProvider
    {
        ICommand CurrentCommand { get; }
    }
}
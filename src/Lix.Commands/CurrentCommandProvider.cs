namespace Lix.Commands
{
    public class CurrentCommandProvider : ICurrentCommandProvider
    {
        public CurrentCommandProvider(ICommand command)
        {
            this.CurrentCommand = command;
        }

        public ICommand CurrentCommand
        {
            get;
            private set;
        }
    }
}
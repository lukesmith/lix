namespace Lix.Commands
{
    public class CommandValidationRule
    {
        public CommandValidationRule(string message)
        {
            this.Message = message;
        }

        public string Message
        {
            get;
            private set;
        }
    }
}
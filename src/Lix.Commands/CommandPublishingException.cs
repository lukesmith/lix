using System;

namespace Lix.Commands
{
    public class CommandPublishingException : Exception
    {
        public CommandPublishingException(string message)
            : base(message)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace Lix.Commands
{
    [Serializable]
    public class CommandPublishingException : Exception
    {
        public CommandPublishingException()
        {
        }

        public CommandPublishingException(string message) : base(message)
        {
        }

        public CommandPublishingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CommandPublishingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
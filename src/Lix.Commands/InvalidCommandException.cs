using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lix.Commands
{
    [Serializable]
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(ICommand command, IEnumerable<CommandValidationRule> validationRules)
        {
            this.Command = command;
            this.ValidationRules = validationRules;
        }

        public InvalidCommandException(string message) : base(message)
        {
        }

        public InvalidCommandException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidCommandException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public ICommand Command
        {
            get;
            private set;
        }

        public IEnumerable<CommandValidationRule> ValidationRules
        {
            get;
            private set;
        }
    }
}
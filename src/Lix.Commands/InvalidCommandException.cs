using System;
using System.Collections.Generic;

namespace Lix.Commands
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(ICommand command, IEnumerable<CommandValidationRule> validationRules)
        {
            this.Command = command;
            this.ValidationRules = validationRules;
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
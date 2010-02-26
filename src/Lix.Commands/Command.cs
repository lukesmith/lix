using System.Collections.Generic;
using System.Linq;

namespace Lix.Commands
{
    public abstract class Command : ICommand
    {
        private IEnumerable<CommandValidationRule> failedValidationRules;

        public virtual IEnumerable<CommandValidationRule> Validate()
        {
            if (this.failedValidationRules == null)
            {
                this.failedValidationRules = Enumerable.Empty<CommandValidationRule>();
            }

            return this.failedValidationRules;
        }

        public bool IsValid
        {
            get { return !this.Validate().Any(); }
        }
    }
}

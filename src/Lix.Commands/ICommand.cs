using System.Collections.Generic;

namespace Lix.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Determines whether the command is valid.
        /// </summary>
        /// <returns>
        /// Returns a collection of <see cref="CommandValidationRule"/> if the command is invalid.
        /// </returns>
        IEnumerable<CommandValidationRule> Validate();

        /// <summary>
        /// Indicates whether the command is valid or invalid.
        /// </summary>
        bool IsValid { get; }
    }
}
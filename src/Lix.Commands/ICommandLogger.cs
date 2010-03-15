using System;

namespace Lix.Commands
{
    public interface ICommandLogger
    {
        void LogSuccess(ICommand command);
        
        void LogFailure(ICommand command, Exception exception);
    }
}
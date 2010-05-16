using System.Collections.Generic;

namespace Lix.Commands
{
    public interface ICommandHandlerContainer
    {
        IEnumerable<ICommandHandler<TCommand>> GetInstances<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
using System.Collections.Generic;

namespace Lix.Commands
{
    public interface ICommandPublisherContainer
    {
        IEnumerable<ICommandHandler<TCommand>> GetInstances<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
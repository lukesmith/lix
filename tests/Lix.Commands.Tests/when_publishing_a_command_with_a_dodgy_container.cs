using System;
using System.Collections.Generic;

namespace Lix.Commands.Tests
{
    internal class CommandPublisherContainerStub : ICommandPublisherContainer
    {
        public IEnumerable<ICommandHandler<TCommand>> GetInstances<TCommand>(TCommand command) where TCommand : ICommand
        {
            throw new NotImplementedException();
        }
    }
}

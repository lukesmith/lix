using System.Collections.Generic;

namespace Lix.Commands
{
    public interface IEventPublisher
    {
        void Publish(IEvent @event);

        void Publish(IEnumerable<IEvent> events);
    }
}
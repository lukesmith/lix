using System;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commands
{
    /// <summary>
    /// Represents an <see cref="IEventPublisher"/> that caches the published <see cref="IEvent"/>'s to be fired at a later time.
    /// </summary>
    public class DelayedEventPublisher : IEventPublisher
    {
        private readonly IEventPublisher eventPublisher;
        private readonly IList<IEvent> events;

        public DelayedEventPublisher(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
            this.events = new List<IEvent>();
        }

        public void PublishAllEvents()
        {
            foreach (var @event in this.events)
            {
                this.eventPublisher.Publish(@event);
            }
        }

        public IEvent[] Events
        {
            get { return this.events.ToArray(); }
        }

        void IEventPublisher.Publish(IEvent @event)
        {
            this.events.Add(@event);
        }

        void IEventPublisher.Publish(IEnumerable<IEvent> @events)
        {
            foreach (var @event in @events)
            {
                this.events.Add(@event);
            }
        }
    }
}
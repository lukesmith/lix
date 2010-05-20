using System;

namespace Lix.Commands
{
    public abstract class AbstractCommandHandler<T> : ICommandHandler<T>
        where T : class, ICommand
    {
        private IEventPublisher eventPublisher;

        public abstract void Execute(T command);

        protected void PublishEvent(IEvent @event)
        {
            this.eventPublisher.Publish(@event);
        }

        void ICommandHandler.Execute(ICommand command)
        {
            this.Execute(command as T);
        }

        void ICommandHandler.SetEventPublisher(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
    }
}
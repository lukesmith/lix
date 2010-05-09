namespace Lix.Commands
{
    public abstract class AbstractCommandHandler<T> : ICommandHandler<T>
        where T : ICommand
    {
        private IEventPublisher eventPublisher;

        public abstract void Execute(T command);

        protected void PublishEvent(IEvent @event)
        {
            this.eventPublisher.Publish(@event);
        }

        void ICommandHandler<T>.SetEventPublisher(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
    }
}
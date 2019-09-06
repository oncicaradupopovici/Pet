using MediatR;
using NBB.Core.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.Application.Services
{
    public interface IEventHub
    {
        IEnumerable<IEvent> GetEvents();
        void AddEvent(IEvent @event);
    }

    class EventHub : IEventHub
    {
        private readonly HashSet<IEvent> _events = new HashSet<IEvent>();

        public EventHub()
        {
        }

        public void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<IEvent> GetEvents() => _events;
    }

    class EventContextLogger : INotificationHandler<INotification>
    {
        private readonly IEventHub _eventHub;

        public EventContextLogger(IEventHub eventHub)
        {
            _eventHub = eventHub;
        }

        public Task Handle(INotification notification, CancellationToken cancellationToken)
        {
            if (notification is IEvent @event)
            {
                _eventHub.AddEvent(@event);
            }
            return Task.CompletedTask;
        }
    }
}

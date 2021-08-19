using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pet.Application.Services
{
    public interface IEventHub
    {
        IEnumerable<INotification> GetEvents();
        void AddEvent(INotification @event);
    }

    class EventHub : IEventHub
    {
        private readonly HashSet<INotification> _events = new HashSet<INotification>();

        public void AddEvent(INotification @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<INotification> GetEvents() => _events;
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
            _eventHub.AddEvent(notification);
            return Task.CompletedTask;
        }
    }
}

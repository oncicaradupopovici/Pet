using NBB.Application.DataContracts;
using NBB.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace Pet.Web.Models
{
    public class SyncCommandResult
    {
        public Guid CommandId { get; }
        public List<EventDescriptor> Events { get; }

        public SyncCommandResult(Guid commandId, List<EventDescriptor> events)
        {
            CommandId = commandId;
            Events = events;
        }

        public static SyncCommandResult From(Command command, IEnumerable<INotification> events)
        {
            return new(Guid.NewGuid(), events.Select(EventDescriptor.From).ToList());
        }

        
    }

    public class EventDescriptor
    {
        public string EventType { get; }
        public INotification Payload { get; }

        public EventDescriptor(string eventType, INotification payload)
        {
            EventType = eventType;
            Payload = payload;
        }

        public static EventDescriptor From(INotification @event)
        {
            return new(@event.GetType().Name, @event);
        }
    }
}

using NBB.Application.DataContracts;
using NBB.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static SyncCommandResult From(Command command, IEnumerable<IEvent> events)
        {
            return new SyncCommandResult(command.Metadata.CommandId, events.Select(EventDescriptor.From).ToList());
        }

        
    }

    public class EventDescriptor
    {
        public string EventType { get; }
        public IEvent Payload { get; }

        public EventDescriptor(string eventType, IEvent payload)
        {
            EventType = eventType;
            Payload = payload;
        }

        public static EventDescriptor From(IEvent @event)
        {
            return new EventDescriptor(@event.GetType().Name, @event);
        }
    }
}

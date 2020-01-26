using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class BongRemovedEvent : Event
    {
        public BongRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
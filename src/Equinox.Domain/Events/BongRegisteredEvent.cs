using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events
{
    public class BongRegisteredEvent : Event
    {
        public BongRegisteredEvent(Guid id, string name, string referenceNo, DateTime arrivingInStock)
        {
            Id = id;
            Name = name;
            ReferenceNo = referenceNo;
            ArrivingInStock = arrivingInStock;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string ReferenceNo { get; private set; }

        public DateTime ArrivingInStock { get; private set; }
    }
}
using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands
{
    public abstract class BongCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string ReferenceNo { get; protected set; }

        public DateTime ArrivingInStock { get; protected set; }
    }
}
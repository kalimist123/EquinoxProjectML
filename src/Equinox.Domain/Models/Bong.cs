using System;
using System.Collections.Generic;
using System.Text;
using Equinox.Domain.Core.Models;

namespace Equinox.Domain.Models
{
    public class Bong : Entity
    {
    
        // Empty constructor for EF
        protected Bong() { }

        public Bong(string name, string referenceNo, DateTime arrivingInStock)
        {
            Name = name;
            ReferenceNo = referenceNo;
            ArrivingInStock = arrivingInStock;
        }

        public string Name { get; private set; }

        public string ReferenceNo { get; private set; }

        public DateTime ArrivingInStock { get; private set; }
    }
}

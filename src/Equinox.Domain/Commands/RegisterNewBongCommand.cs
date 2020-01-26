using System;
using Equinox.Domain.Models;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RegisterNewBongCommand : BongCommand
    {
        public RegisterNewBongCommand(string name, string referenceNo, DateTime arrivingInStock)
        {
            Name = name;
            ReferenceNo = referenceNo;
            ArrivingInStock = arrivingInStock;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewBongCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class UpdateBongCommand : BongCommand
    {
        public UpdateBongCommand(Guid id, string name, string referenceNo, DateTime arrivingInStock)
        {
            Id = id;
            Name = name;
            ReferenceNo = referenceNo;
            ArrivingInStock =arrivingInStock;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateBongCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
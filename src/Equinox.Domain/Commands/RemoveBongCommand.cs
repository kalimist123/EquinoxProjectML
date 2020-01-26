using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RemoveBongCommand : BongCommand
    {
        public RemoveBongCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveBongCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
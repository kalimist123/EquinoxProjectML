using System;
using Equinox.Domain.Commands;
using FluentValidation;

namespace Equinox.Domain.Validations
{
    public abstract class BongValidation<T> : AbstractValidator<T> where T : BongCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        protected void ValidateArrivingInStock()
        {
            RuleFor(c => c.ArrivingInStock)
                .NotEmpty();

        }

        protected void ValidateReferenceNo()
        {
            RuleFor(c => c.ReferenceNo)
                .NotEmpty().WithMessage("Please ensure you have entered the reference no")
                .Length(2, 150).WithMessage("The Reference No must have between 2 and 150 characters");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

       
    }
}
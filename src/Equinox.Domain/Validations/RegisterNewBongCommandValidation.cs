using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RegisterNewBongCommandValidation : BongValidation<RegisterNewBongCommand>
    {
        public RegisterNewBongCommandValidation()
        {
            ValidateName();
            ValidateArrivingInStock();
            ValidateReferenceNo();
        }
    }
}
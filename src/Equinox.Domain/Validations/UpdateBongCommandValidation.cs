using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class UpdateBongCommandValidation : BongValidation<UpdateBongCommand>
    {
        public UpdateBongCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateArrivingInStock();
            ValidateReferenceNo();
        }
    }
}
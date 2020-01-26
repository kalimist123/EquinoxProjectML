using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RemoveBongCommandValidation : BongValidation<RemoveBongCommand>
    {
        public RemoveBongCommandValidation()
        {
            ValidateId();
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Equinox.Infra.CrossCutting.Identity.Authorization
{
    public class ClaimsRequirementHandler : AuthorizationHandler<ClaimRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       ClaimRequirement requirement)
        {

            //var claim = context.User.Claims.FirstOrDefault(c => c.Type == requirement.ClaimName);
            var claim = context.User.Claims.FirstOrDefault(c => c.Type == requirement.ClaimName && c.Value ==requirement.ClaimValue);
           // if (claim != null && claim.Value.Contains(requirement.ClaimValue))

                if (claim != null)
                {
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
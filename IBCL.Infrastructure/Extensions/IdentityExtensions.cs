using System.Security.Claims;
using System.Security.Principal;

namespace IBCL.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity) { 
        
        ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            var currentUserId= claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (currentUserId is null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(currentUserId);
        }
    }
}

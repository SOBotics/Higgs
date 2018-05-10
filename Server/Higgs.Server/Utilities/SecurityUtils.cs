using System.Linq;
using System.Security.Claims;

namespace Higgs.Server.Utilities
{
    public static class SecurityUtils
    {
        public const string ACCOUNT_ID_CLAIM = "accountId";

        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userIdStr = user.Claims.Where(c => c.Type == ACCOUNT_ID_CLAIM).Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(userIdStr) && int.TryParse(userIdStr, out var userId))
                return userId;
            return null;
        }

        public static bool HasClaim(this ClaimsPrincipal user, string claim)
        {
            return user.HasClaim(claim, string.Empty);
        }
    }
}

using System.Security.Claims;

namespace Site.EndPoint.Models.Utility
{
    public static class ClaimUtility
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            var claims = user.Identity as ClaimsPrincipal;
            string userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}

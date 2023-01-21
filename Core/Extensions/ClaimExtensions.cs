using Core.Utilities.Security.Jwt;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddUserEmail(this ICollection<Claim> claims, string userEmail)
        {
            claims.Add(new Claim(CustomClaimTypes.UserEmail, userEmail));
        }

        public static void AddUserName(this ICollection<Claim> claims, string userName)
        {
            claims.Add(new Claim(CustomClaimTypes.UserName, userName));
        }

        public static void AddUserId(this ICollection<Claim> claims, string userId)
        {
            claims.Add(new Claim(CustomClaimTypes.UserId, userId));
        }
    }
}
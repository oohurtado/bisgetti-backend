using Microsoft.IdentityModel.Tokens;
using Shared.Models.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Common
{
    public static class CustomToken
    {
        public static UserToken BuildToken(List<Claim> claims, string role, string jwtKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(365);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var userToken = new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Role = role
            };

            return userToken;
        }

        public static List<Claim> GetClaims(string userId, int personId, string email, string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(Shared.Common.ClaimTypes.UserId, userId),
                new Claim(Shared.Common.ClaimTypes.PersonId, personId.ToString()),
            };

            return claims;
        }
    }
}

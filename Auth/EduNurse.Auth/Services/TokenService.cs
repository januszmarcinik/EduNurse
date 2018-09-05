using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using EduNurse.Auth.Entities;
using EduNurse.Auth.Settings;
using Microsoft.IdentityModel.Tokens;

namespace EduNurse.Auth.Services
{
    internal class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public TokenResult CreateToken(User user)
        {
            var now = DateTime.UtcNow;

            var roles = user.IsAdmin
                ? Enum.GetNames(typeof(Role))
                : user.Roles.Select(r => r.ToString());
            var rolesClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, GetTimeStamp(now).ToString(), ClaimValueTypes.Integer64)
            }.Concat(rolesClaims);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256);
            var expiry = now.AddMinutes(_jwtSettings.ExpiryMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expiry,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResult(token, expiry);
        }

        private static long GetTimeStamp(DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = dateTime.Subtract(new TimeSpan(epoch.Ticks));

            return time.Ticks / 10000;
        }
    }
}

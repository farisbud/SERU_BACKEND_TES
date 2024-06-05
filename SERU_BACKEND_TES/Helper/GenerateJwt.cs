using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SERU_BACKEND_TES.Models;
using SERU_BACKEND_TES.ViewModels.JwtSetting;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SERU_BACKEND_TES.Helper
{
    public class GenerateJwt
    {

        private readonly JwtSettings _jwtSettings;
        public GenerateJwt(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(Users user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.name),
                    new Claim(ClaimTypes.Role, user.is_admin ? "admin" : "user")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return tokenhandler.WriteToken(token);
        }

        public string GenerateCookies(Users user)
        {

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.refreshKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.name)
                }),
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return tokenhandler.WriteToken(token);

        }

        public async Task<JwtSecurityToken> Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = await tokenHandler.ValidateTokenAsync(jwt, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.refreshKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
            });

            return (JwtSecurityToken)principal.SecurityToken;
        }

        public string GenerateRefreshToken(Users user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.securityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.name),
                    new Claim(ClaimTypes.Role, user.is_admin ? "admin" : "user")
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return tokenhandler.WriteToken(token);
        }
    }
}

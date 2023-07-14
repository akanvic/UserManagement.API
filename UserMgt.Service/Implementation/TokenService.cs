using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core.Config;
using UserMgt.Core.Entities.Models;
using UserMgt.Service.Interface;

namespace UserMgt.Service.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwt;
        public TokenService(IOptions<JwtSettings> jwt)
        {
            _jwt = jwt.Value;
        }
        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.SecretKey!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress!)
                    // Add more claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Set token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwt.Audience1,
                Issuer = _jwt.Issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Core.Entities.Identity;
using Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Intrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }
        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName,appUser.DisplayName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var tokenDesctiption = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(7),
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Issuer"]
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            var token = tokenHandle.CreateToken(tokenDesctiption);
            return tokenHandle.WriteToken(token);
        }
    }
}

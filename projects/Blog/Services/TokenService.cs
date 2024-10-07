using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Services
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(UserModel user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier , user.UserId.ToString()),
                    new Claim(ClaimTypes.Name , user.UserName),
                    new Claim(ClaimTypes.Email ,user.Email )
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials =new SigningCredentials
                (
                    new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
    }
}
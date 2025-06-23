using ApiTest.Commons;
using ApiTest.Models;
using ApiTest.Models.Dtos;
using ApiTest.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiTest.Services.Implementaciones
{
    public class JwtService : IJwtService
    {
        private readonly string? secretKey;
        private readonly string? issuer;
        public JwtService(IConfiguration config)
        {
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
            issuer = config.GetSection("settings").GetSection("issuer").ToString();
        }
        public string GenerarToken(Usuario user)
        {
            string token = "";
            try
            {
                
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));

                var tokenDesc = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(25),
                    Issuer = issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDesc);

                token = tokenHandler.WriteToken(tokenConfig);

                return token;
            }
            catch (Exception ex) 
            {
                return token;
            }

        }

    }
}

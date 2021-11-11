using BattleShip.BussinessLayer.Interfaces;
using BattleShip.BussinessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BattleShip.BussinessLayer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        private readonly SymmetricSecurityKey symmetricKey;
        private readonly string issuer; // token issuer
        private readonly string audience; // tolen subscriber   
        private readonly int lifetime; // token lifetime


        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.audience = this.configuration["Jwt:Audience"];
            this.issuer = this.configuration["Jwt:Issuer"];
            this.lifetime = int.Parse(this.configuration["Jwt:Lifetime"]);
            this.symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration["Jwt:Key"]));
        }

        public string CreateToken(UserDTO userDTO)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userDTO.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, userDTO.Email),                   
                };

            var credentials = new SigningCredentials(this.symmetricKey, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                   issuer: this.issuer,
                   audience: this.audience,
                   claims: claims,
                   expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(this.lifetime)),
                   signingCredentials: credentials);              

            return new JwtSecurityTokenHandler().WriteToken(jwt);           
        }
    }
}

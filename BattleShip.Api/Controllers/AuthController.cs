using BattleShip.BussinessLayer.Models;
using BattleShip.BussinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BattleShip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var secretKey = AuthService.GetSymmetricSecurityKey();

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: AuthService.ISSUER,
                audience: AuthService.AUDIENCE,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(AuthService.LIFETIME),
                signingCredentials: signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }
    }
}

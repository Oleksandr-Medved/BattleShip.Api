using BattleShip.BussinessLayer.Models;
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

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretkey@345"));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5036",
                audience: "http://localhost:5036",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCredentials);


            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }
    }
}

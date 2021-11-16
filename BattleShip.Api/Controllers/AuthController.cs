using BattleShip.BussinessLayer.Interfaces;
using BattleShip.BussinessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ITokenService jwtService;

        private readonly ILogger<AuthController> logger;

        private readonly IUserService userService;

        public AuthController(ITokenService authService, IUserService userService, ILogger<AuthController> logger)
        {
            this.jwtService = authService;
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            this.logger.LogInformation($"Hit Login Method - User: {userDTO.Name}");

            var userIsValid = await this.userService.IfUserExist(userDTO);

            if (!userIsValid)
            {
                this.logger.LogError("User is invalid");

                return BadRequest("Invalid client request");
            }

            var tokenString = this.jwtService.CreateToken(userDTO);

            userDTO.Token = tokenString;

            this.logger.LogInformation("Token was generated");

            this.userService.AddNewUser(userDTO);

            return Ok(new { Token = tokenString });
        }
    }
}

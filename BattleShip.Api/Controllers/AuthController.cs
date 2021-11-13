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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            this.logger.LogInformation($"Hit Login Method - User: {user.Email}");

            var userIsValid = await this.userService.Validate(user);

            if (!userIsValid)
            {
                this.logger.LogError("User is invalid");

                return BadRequest("Invalid client request");
            }

            var tokenString = this.jwtService.CreateToken(user);

            this.logger.LogInformation("Token was generated");

            return Ok(new { UserEmail = user.Email , Token = tokenString });
        }
    }
}

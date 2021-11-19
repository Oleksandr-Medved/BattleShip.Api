using BattleShip.BussinessLayer.Interfaces;
using BattleShip.BussinessLayer.Models;
using BattleShip.Core;
using Microsoft.AspNetCore.Mvc;

namespace BattleShip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService jwtService;

        private readonly ILogger<AuthController> logger;

        private readonly IUserService userService;

        private readonly JsonSerialization jsonSerialization;

        public AuthController(ITokenService authService,
            IUserService userService,
            ILogger<AuthController> logger,
            JsonSerialization jsonSerialization)
        {
            this.jwtService = authService;
            this.userService = userService;
            this.logger = logger;
            this.jsonSerialization = jsonSerialization;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDtO)
        {
            this.logger.LogInformation($"Hit Login Method - User: {loginDtO.Name}");

            if (!ModelState.IsValid)
            {
                return BadRequest("Login model is not valid");
            }

            var isUserValid = await this.userService.Validate(loginDtO);

            if (!isUserValid)
            {
                return BadRequest("Login is not valid");
            }

            var tokenString = this.jwtService.CreateToken(loginDtO);

            this.logger.LogInformation("Token was generated");

            var token = this.jsonSerialization.Serialize(tokenString);

            return Ok(token);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            this.logger.LogInformation($"Hit Register Method - User: {registerDTO.Name}");

            if (!this.ModelState.IsValid || registerDTO.ConfirmedPassword != registerDTO.Password)
                return BadRequest("Model is not valid");

            var userName = this.userService.AddNewUser(registerDTO);

            userName = this.jsonSerialization.Serialize(userName);

            return Ok(userName);
        }
    }
}

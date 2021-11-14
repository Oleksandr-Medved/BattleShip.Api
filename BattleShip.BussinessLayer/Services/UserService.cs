using BattleShip.BussinessLayer.Interfaces;
using BattleShip.BussinessLayer.Models;
using Microsoft.Extensions.Logging;

namespace BattleShip.BussinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;

        public UserService(ILogger<UserService> logger)
        {
            this.logger = logger;
        }

        public async Task<UserDTO> GetUserByName(string userName)
        {
            return await Task.FromResult
                (new UserDTO()
                {
                    Name = userName,
                    Email = "email",
                    Password = "passsword"
                });
        }

        public async Task<bool> Validate(LoginDTO user)
        {
            this.logger.LogInformation("Validate User");

            return await Task.FromResult(true);
        }
    }
}

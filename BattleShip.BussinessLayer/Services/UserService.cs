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

        public async Task<bool> Validate(UserDTO user)
        {
            this.logger.LogInformation("Validate User");

            return await Task.FromResult(true);
        }
    }
}

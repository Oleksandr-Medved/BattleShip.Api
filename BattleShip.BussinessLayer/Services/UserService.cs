using BattleShip.BussinessLayer.Interfaces;
using BattleShip.BussinessLayer.Models;
using BattleShip.DataAccessLayer.Models;
using BattleShip.DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace BattleShip.BussinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;

        private IRepository<DataAccessLayer.Models.User> repo;

        public UserService(ILogger<UserService> logger, IRepository<DataAccessLayer.Models.User> repo)
        {
            this.logger = logger;
            this.repo = repo;
        }

        public string AddNewUser(RegisterDTO userDTO)
        {
            var user = this.EncyptedUserPassword(userDTO);

            this.repo.Add(user);

            return user.Name;
        }

        public async Task<bool> Validate(LoginDTO loginDTO)
        {
            this.logger.LogInformation("Validate user");

            var user = await this.repo.GetEntityBy(x => x.Name == loginDTO.Name);                      

            return user != null && BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
        }

        private User EncyptedUserPassword(RegisterDTO userDTO)
        {
            this.logger.LogInformation("Encrypting user' password");

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            return new User()
            {
                Name = userDTO.Name,
                Password = hashPassword
            };
        }
    }
}

using BattleShip.BussinessLayer.Interfaces;
using BattleShip.BussinessLayer.Models;
using BattleShip.DataAccessLayer.Models;
using BattleShip.DataAccessLayer.Repositories;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace BattleShip.BussinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;

        private IRepository<User> repo;

        private ValidateUser validate;

        public UserService(ILogger<UserService> logger, IRepository<User> repo, ValidateUser validate)
        {
            this.logger = logger;
            this.repo = repo;
            this.validate = validate;
        }

        public async Task<bool> Validate<T>(T userDTO) where T : UserDTO
        {
            this.logger.LogInformation("Validate user");

            return await this.validate.Validate(userDTO);
        }

        public string AddNewUser(RegisterDTO userDTO)
        {
            var user = this.EncyptedUserPassword(userDTO);

            this.repo.Add(user);

            return user.Name;
        }

        private User EncyptedUserPassword(RegisterDTO userDTO)
        {
            this.logger.LogInformation("Encrypting user's password");

            var hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(userDTO.Password, hashType: HashType.SHA384);

            return new User()
            {
                Name = userDTO.Name,
                Password = hashPassword
            };
        }
    }
}

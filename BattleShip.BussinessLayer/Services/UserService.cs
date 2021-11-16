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

        private IRepository<User> repo;

        public UserService(ILogger<UserService> logger, IRepository<User> repo)
        {
            this.logger = logger;
            this.repo = repo;
        }

        public async Task<UserDTO> GetUserByName(string userName)
        {
            var user = await this.repo.GetBy(x => x.Name == userName);

            if (user == null || !user.Any()) return new UserDTO();

            return new UserDTO
            {
                Name = user.FirstOrDefault().Name
            };
        }

        public async Task<bool> IfUserExist(UserDTO user)
        {
            this.logger.LogInformation("Validate User");

            var userDTO = await this.repo.GetBy(x => x.Name == user.Name);

            return userDTO == null || !userDTO.Any();
        }

        public void AddNewUser(UserDTO userDTO)
        {
            var user = new User()
            {
                Name = userDTO.Name,
                Password = userDTO.Password,
                Token = userDTO?.Token
            };

            this.repo.Add(user);
        }
    }
}

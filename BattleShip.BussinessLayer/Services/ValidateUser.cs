using BattleShip.BussinessLayer.Models;
using BattleShip.DataAccessLayer.Models;
using BattleShip.DataAccessLayer.Repositories;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace BattleShip.BussinessLayer.Services
{
    public class ValidateUser
    {
        private readonly ILogger<UserService> logger;

        private readonly Dictionary<string, Func<UserDTO, Task<bool>>> validateDict;

        private readonly IRepository<User> repo;

        public ValidateUser(IRepository<User> repo, ILogger<UserService> logger)
        {
            this.repo = repo;

            this.logger = logger;

            this.validateDict = new Dictionary<string, Func<UserDTO, Task<bool>>>();

            this.validateDict.Add(nameof(LoginDTO), ValidateLogin);

            this.validateDict.Add(nameof(RegisterDTO), ValidateRegister);
        }

        public async Task<bool> Validate<T>(T userDTO) where T: UserDTO
        {
            Type type = typeof(T);

            if (this.validateDict.ContainsKey(type.Name))
            {
                return await this.validateDict[type.Name](userDTO);
            }

            return false;
        }

        private async Task<bool> ValidateLogin(UserDTO userDTO)
        {
            var haspassword = BCrypt.Net.BCrypt.EnhancedHashPassword(userDTO.Password,
                hashType: HashType.SHA384);

            var user = await this.repo.GetEntityBy(x => x.Name == userDTO.Name);

            return user != null
                && BCrypt.Net.BCrypt.EnhancedVerify(userDTO.Password,
                user.Password,
                hashType: HashType.SHA384);
        }

        private async Task<bool> ValidateRegister(UserDTO userDTO)
        {
            var user = await this.repo.GetEntityBy(x => x.Name == userDTO.Name);

            return user == null;
        }

    }
}

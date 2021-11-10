using BattleShip.BussinessLayer.Interfaces;
using BattleShip.BussinessLayer.Models;

namespace BattleShip.BussinessLayer.Services
{
    public class UserService : IUserService
    {
        public Task<UserDTO> Authenticate(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}

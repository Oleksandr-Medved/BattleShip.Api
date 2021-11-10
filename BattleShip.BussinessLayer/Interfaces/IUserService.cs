using BattleShip.BussinessLayer.Models;

namespace BattleShip.BussinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Authenticate(UserDTO user);
    }
}

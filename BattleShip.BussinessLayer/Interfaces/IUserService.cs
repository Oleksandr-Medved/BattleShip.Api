using BattleShip.BussinessLayer.Models;

namespace BattleShip.BussinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<bool> IfUserExist(UserDTO user);
        Task<UserDTO> GetUserByName(string userName);
        void AddNewUser(UserDTO userDTO);
    }
}

using BattleShip.BussinessLayer.Models;

namespace BattleShip.BussinessLayer.Interfaces
{
    public interface IUserService
    {
        string AddNewUser(RegisterDTO registerDTO);

        Task<bool> Validate<T>(T userDTO) where T : UserDTO;
    }
}

using BattleShip.BussinessLayer.Models;

namespace BattleShip.BussinessLayer.Interfaces
{
    public interface ITokenService
    {
        string CreateToken<T>(T userDTO) where T : UserDTO;
    }
}

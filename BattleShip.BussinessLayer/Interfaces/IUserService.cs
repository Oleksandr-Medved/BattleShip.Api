using BattleShip.BussinessLayer.Models;

namespace BattleShip.BussinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<bool> Validate(LoginDTO user);
    }
}

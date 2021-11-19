using System.Threading.Tasks;
using BattleShip.BussinessLayer.Models;

namespace BattleShip.BussinessLayer.Interfaces
{
    public interface IUserService
    {
        string AddNewUser(RegisterDTO registerDTO);

        Task<bool> Validate(LoginDTO loginDTO);
    }
}

namespace BattleShip.BussinessLayer.Models
{
    public abstract class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}


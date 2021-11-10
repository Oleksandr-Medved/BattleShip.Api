using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace BattleShip.BussinessLayer.Services
{
    public class AuthService
    {
        public const string ISSUER = "http://localhost:5036"; // издатель токена
        public const string AUDIENCE = "http://localhost:5036"; // потребитель токена        
        public const int LIFETIME = 5; // время жизни токена - 5 минута
        private const string KEY = "superSecretkey@345";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

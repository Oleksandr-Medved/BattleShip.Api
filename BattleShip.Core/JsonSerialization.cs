using System.Text.Json;

namespace BattleShip.Core
{
    public class JsonSerialization
    {
        public string Serialize<T>(T entity)
        {
            return JsonSerializer.Serialize(entity);
        }

        public string Serialize<T>(List<T> entities)
        {
            return JsonSerializer.Serialize(entities);
        }
    }
}
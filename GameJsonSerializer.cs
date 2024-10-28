using System.Text.Json;

namespace Scoreboard
{
    public class GameJsonSerializer
    {
        public event EventHandler<string>? DataSerialized;
        public GameJsonSerializer(Game game)
        {
            game.UpdateGame += (sender, data) => SerializeData(data);
        }

        private void SerializeData(GameEventArgs data)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(data);
                Console.WriteLine(jsonString);
                DataSerialized?.Invoke(this, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Serialization falied: {e}");
            }


        }
    }
}
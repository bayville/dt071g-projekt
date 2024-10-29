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

        private async void SerializeData(GameEventArgs data)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(data);
                // Console.WriteLine(jsonString);
                await Task.Run(() => DataSerialized?.Invoke(this, jsonString));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Serialization failed: {e}");
            }
        }
    }
}

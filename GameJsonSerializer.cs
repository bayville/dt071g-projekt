using System.Text.Json;

namespace Scoreboard
{
    public class GameJsonSerializer
    {
        // Event that triggers when data has been serialized
        public event EventHandler<string>? DataSerialized;

        public GameJsonSerializer(Game game)
        {
            game.UpdateGame += (sender, data) => SerializeData(data); // Listens to game update event
        }


        // Serializes the data sent from event and triggers event
        private async void SerializeData(GameEventArgs data)
        {
            try
            {   
                // Serializes data
                string jsonString = JsonSerializer.Serialize(data);
                
                // Triggers event
                await Task.Run(() => DataSerialized?.Invoke(this, jsonString));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Serialization failed: {e}");
            }
        }
    }
}

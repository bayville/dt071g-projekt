using System.Text.Json;

namespace Scoreboard
{
    public static class GameStateLoader
    {
        private static readonly string BackupPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "backup",
            "gamedata.json"
        );

        // Tries to restore previous game by reading json file. 
        public static GameEventArgs? LoadLastState()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(BackupPath)!);


                if (!File.Exists(BackupPath))
                {
                    Console.WriteLine("No saved state found.");
                    return null;
                }

                // Reads file data
                string jsonData = File.ReadAllText(BackupPath);
                GameEventArgs gameState = JsonSerializer.Deserialize<GameEventArgs>(jsonData)!;
                Console.WriteLine("Laddning av tidigare matchdata lyckades.");
                return gameState;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game state: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }

                return null;
            }
        }
    }
}

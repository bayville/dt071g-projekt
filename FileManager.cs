
namespace Scoreboard
{

    public class FileManager
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup", "gamedata.json");

        public FileManager(GameJsonSerializer json)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            json.DataSerialized += OnDataSerialized;
        }

        private void OnDataSerialized(object? sender, string jsonData)
        {
            Task.Run(() => WriteToFile(jsonData));
        }

        private void WriteToFile(string jsonData)
        {
            try
            {
                string temporaryFile = _filePath + ".temp"; //Create temporary file
                File.WriteAllText(temporaryFile, jsonData);

                File.Move(temporaryFile, _filePath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write data to file: {ex.Message}");
            }
        }
    }

}

namespace Scoreboard
{

    public class FileManager
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup", "gamedata.json");
        private bool _isWriting;
        public FileManager(GameJsonSerializer json)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            json.DataSerialized += OnDataSerialized;
        }

        private void OnDataSerialized(object? sender, string jsonData)
        {
            if (!_isWriting)
            {
                Task.Run(() => WriteToFile(jsonData));
            }
        }

        private void WriteToFile(string jsonData)
        {
            try
            {
                _isWriting = true;
                string temporaryFile = Path.Combine(
                    Path.GetDirectoryName(_filePath)!, $"gamedata_.tmp");

                File.WriteAllText(temporaryFile, jsonData);

                File.Move(temporaryFile, _filePath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write data to file: {ex.Message}");
            }
            finally
            {
                _isWriting = false;
            }
        }
    }
}

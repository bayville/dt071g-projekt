
namespace Scoreboard
{

    public class FileManager
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup", "gamedata.json");
        private bool _isWriting;
        public FileManager(GameJsonSerializer json)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            json.DataSerialized += OnDataSerialized; // Listen to event if Json is Serialized
        }

        // Runs a task to write data to file if its not already running
        private void OnDataSerialized(object? sender, string jsonData)
        {
            if (!_isWriting)
            {
                Task.Run(() => WriteToFile(jsonData));
            }
        }

        // Writes file to data
        private void WriteToFile(string jsonData)
        {
            try
            {
                _isWriting = true;
                // Creates a temporary file
                string temporaryFile = Path.Combine(
                    Path.GetDirectoryName(_filePath)!, $"gamedata_.tmp");

                // Writes data to temporary file
                File.WriteAllText(temporaryFile, jsonData);

                // Writes over file with the temporary file
                File.Move(temporaryFile, _filePath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write data to file: {ex.Message}");
            }
            finally
            {
                // If succesful reset is writing bool
                _isWriting = false;
            }
        }
    }
}

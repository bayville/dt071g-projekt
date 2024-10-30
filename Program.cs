namespace Scoreboard
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Try to load previous game data
            var savedState = GameStateLoader.LoadLastState();

            // Get gameSettings 
            var (settings, isRestore, restoreData) = StartUpDialog.Start(savedState);

            // New game with gameSettings
            Game game = new Game(settings, isRestore, restoreData);
            // New instance of control display

            // New instance of controller
            Controller controller = new Controller(game);
            
            //New instance of jsonSerializer 
            GameJsonSerializer jsonSerializer = new GameJsonSerializer(game);
            
            // New instance of filemanager
            _ = new FileManager(jsonSerializer);

            // New instance of UdpTransmitter
            _ = new UdpTransmitter(jsonSerializer);

            game.Update();
            // Listen for keypresses in controller runs as a task
            await controller.ListenToKeyPress();
        }

    }
}
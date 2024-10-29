namespace Scoreboard {
    public class Program {
        static async Task Main(string[] args) {

            var savedState = GameStateLoader.LoadLastState();
            
            var (settings, isRestore, restoreData) = StartUpDialog.Start(savedState);

            
            Game game = new Game(settings, isRestore, restoreData);
            _ = new ConsoleDisplay(game);
            Controller controller = new Controller(game);
            GameJsonSerializer jsonSerializer = new GameJsonSerializer(game);
            _ = new FileManager(jsonSerializer);

            await controller.ListenToKeyPress();
            }

    }
}
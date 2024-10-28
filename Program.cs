
namespace Scoreboard {
    public class Program {
        static async Task Main(string[] args) {
            GameSettings settings = StartUpDialog.Start();
            Game game = new Game(settings);
            _ = new ConsoleDisplay(game);
            Controller controller = new Controller(game);
            _ = new GameJsonSerializer(game);

            await controller.ListenToKeyPress();
            }

    }
}
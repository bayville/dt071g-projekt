namespace Scoreboard {
    public class Program {
        static async Task Main(string[] args) {
            GameSettings settings = new GameSettings(TimeSpan.FromMinutes(20), 3, 5, true);
            Game game = new Game(settings);
            ConsoleDisplay display = new ConsoleDisplay(game);
            Controller controller = new Controller(game);

            await controller.ListenToKeyPress();
        }

    }
}
namespace Scoreboard {
    public class Program {
        static async Task Main(string[] args) {
            GameSettings settings = new GameSettings(20, 3, 5, true);
            Game game = new Game(settings);
            Controller controller = new Controller(game);

            await controller.ListenToKeyPress();
        }

    }
}
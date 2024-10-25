namespace Scoreboard {
    public class Program {
        static async Task Main(string[] args) {
            Game game = new Game();
            var controller = new Controller(game);

            await controller.ListenToKeyPress();
        }

    }
}
using System.Runtime.InteropServices;

namespace Scoreboard
{
    public class Controller
    {
        private readonly Game game;

        public Controller(Game game)
        {
            this.game = game;
        }
        public async Task ListenToKeyPress()
        {
            Console.WriteLine("Press 's' to start, 'p' to pause, 'q' to quit.");

            await Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadKey(true).Key;

                    switch (input)
                    {
                        case ConsoleKey.S:
                            game.Start();
                            break;

                        case ConsoleKey.P:
                            game.Stop();
                            break;

                        case ConsoleKey.Q:
                            Console.WriteLine("Quit");
                            break;

                        case ConsoleKey.A:
                            Console.WriteLine("Skriv antal sekunder");
                            double adjustment = double.Parse(Console.ReadLine());
                            TimeSpan timeAdjustment = TimeSpan.FromSeconds(adjustment);
                            game.AdjustTime(timeAdjustment);
                            break;

                        case ConsoleKey.N:
                            game.NewTimer();
                            break;

                        default:
                            Console.WriteLine("Ogiltig tangent. Försök igen.");
                            break;
                    }

                }
            });
        }
    }

}
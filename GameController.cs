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
            Console.WriteLine("\nCONTROLS:\n");
            Console.WriteLine("S to start\nP to pause\nQ to quit\nA to adjust time\nN for new period\n");

            await Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadKey(true);

                    switch (input.Key)
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
                        case ConsoleKey.T:
                            game.ActivateTimeOut();
                            break;
                        case ConsoleKey.O:
                            game.ActivateGameTime();
                            break;

                        case ConsoleKey.A:
                            Console.WriteLine("Skriv antal sekunder");
                            double adjustment = double.Parse(Console.ReadLine());
                            TimeSpan timeAdjustment = TimeSpan.FromSeconds(adjustment);
                            game.AdjustTime(timeAdjustment);
                            break;
                        
                        case ConsoleKey.I:
                            game.ActivateIntermisson();
                            break;

                        case ConsoleKey.H:
                            if (input.Modifiers.HasFlag(ConsoleModifiers.Shift))
                            {
                                game.RemoveGoal(0);
                            }
                            else
                            {
                                game.AddGoal(0);
                            }
                            break;

                        case ConsoleKey.G:
                            if (input.Modifiers.HasFlag(ConsoleModifiers.Shift))
                            {
                                game.RemoveGoal(1);
                            }
                            else
                            {
                                game.AddGoal(1);
                            }

                            break;

                        case ConsoleKey.N:
                            game.NewPeriodTimer(game.Settings.PeriodLength);
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
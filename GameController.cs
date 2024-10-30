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
                            game.Stop();
                            Console.WriteLine("Skriv in antal sekunder att justera, sätt '-' framför siffran för att ange negativt värde");
                            double adjustment = double.Parse(Console.ReadLine());
                            TimeSpan timeAdjustment = TimeSpan.FromSeconds(adjustment);
                            game.AdjustTime(timeAdjustment);
                            break;
                        
                        case ConsoleKey.I:
                            game.ActivateIntermission();
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
                            if (input.Modifiers.HasFlag(ConsoleModifiers.Shift))
                            {
                                game.PreviousPeriod();
                            }
                            else
                            {
                                game.NextPeriod();
                            }
                            break;
                        case ConsoleKey.U:
                            game.AddNewPenalty(0);
                            break;
                        case ConsoleKey.D9:
                            game.AddNewPenalty(1);
                            break;

                        case ConsoleKey.D0:
                            game.SetPenaltyRemainingTime();
                            break;
                        
                        case ConsoleKey.D5:
                            game.RemoveFinishedPenalties();
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
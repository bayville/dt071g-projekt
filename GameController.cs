namespace Scoreboard
{
    public class Controller
    {
        private Game game;
        private int team;
        private int index;
        private int minutes;

        private double seconds; 
        private int intSeconds;

        private int milliseconds; 
        private TimeSpan newPenaltyTime;

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
                        case ConsoleKey.Spacebar:
                            if (game.GameClock.ActiveTimer.IsRunning)
                            {
                                game.GameClock.StopActiveClock();
                            }
                            else
                            {
                                game.GameClock.StartActiveClock();
                            }
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
                        case ConsoleKey.P:
                            game.ActivatePowerbreak();
                            break;

                        case ConsoleKey.A:
                            game.GameClock.StopActiveClock(); ;
                            Console.WriteLine("Skriv in antal sekunder att justera, sätt '-' framför siffran för att dra tillbaka klockan");
                            double adjustment = double.Parse(Console.ReadLine());
                            TimeSpan timeAdjustment = TimeSpan.FromSeconds(adjustment);
                            game.GameClock.AdjustActiveClockTime(timeAdjustment);
                            break;

                        case ConsoleKey.R:
                            bool removed = false;
                            while (!removed)
                            {
                                Console.WriteLine("Radera utvisning");
                                Console.WriteLine("Välj lag att radera utvinsing för:");
                                team = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ange index för utvisning [i]");
                                index = int.Parse(Console.ReadLine());
                                removed = game.GamePenalties.RemovePenaltyWithIndex(index, team);
                            }
                            break;
                        case ConsoleKey.M:
                            bool moved = false;
                            while (!moved)
                            {
                                Console.WriteLine("Flytta utvisning till toppen");
                                Console.WriteLine("Välj lag att flytta utvinsing för:");
                                team = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ange index för utvisning [i]");
                                index = int.Parse(Console.ReadLine());
                                moved = game.GamePenalties.MovePenaltyToTop(index, team);
                            }
                            break;
                        case ConsoleKey.E:
                            Console.WriteLine("Ändra tid för utvisning");
                            Console.WriteLine("Välj lag att ändra utvinsing för:");
                            team = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ange index för utvisning [i]");
                            index = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ange minuter (ex: 11)");
                            minutes = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ange sekunder (ex: 30 | ex2: 20,4)");
                            seconds = double.Parse(Console.ReadLine());
                            intSeconds = (int)seconds;
                            milliseconds = (int)((seconds - intSeconds) * 1000);
                            newPenaltyTime = new TimeSpan(0, 0, minutes, intSeconds, milliseconds);
                            game.GamePenalties.SetPenaltyRemainingTime(index, newPenaltyTime, team);
                            break;
                        case ConsoleKey.U:
                            Console.WriteLine("Lägg till ny utvisning");
                            Console.WriteLine("Välj lag:");
                            team = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ange spelarnummer:");
                            int playerNumber = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ange minuter (ex: 11)");
                            minutes = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ange sekunder (ex: 30 | ex2: 20,4)");
                            seconds = double.Parse(Console.ReadLine());
                            intSeconds = (int)seconds;
                            milliseconds = (int)((seconds - intSeconds) * 1000);
                            newPenaltyTime = new TimeSpan(0, 0, minutes, intSeconds, milliseconds);
                            game.GamePenalties.AddNewPenalty(playerNumber, newPenaltyTime, team);
                            break;
                        case ConsoleKey.I:
                            game.ActivateIntermission();
                            break;

                        case ConsoleKey.H:
                            if (input.Modifiers.HasFlag(ConsoleModifiers.Shift))
                            {
                                game.GameScore.RemoveGoal(0);
                            }
                            else
                            {
                                game.GameScore.AddGoal(0);
                            }
                            break;

                        case ConsoleKey.G:
                            if (input.Modifiers.HasFlag(ConsoleModifiers.Shift))
                            {
                                game.GameScore.RemoveGoal(1);
                            }
                            else
                            {
                                game.GameScore.AddGoal(1);
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



                        default:
                            Console.WriteLine("Ogiltig tangent. Försök igen.");
                            break;
                    }

                }
            });
        }
    }

}
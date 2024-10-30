namespace Scoreboard
{
    public class Controller
    {
        private Game game;
        private int team;
        private int index;
        private bool cancel;
        private bool success;
        private bool confirmed;

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
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                (confirmed, cancel) = ConsoleDialogs.Confirm(false);
                                if (confirmed)
                                {
                                    Console.WriteLine("Quit");
                                }
                            }
                            break;

                        case ConsoleKey.T:
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                game.ActivateTimeOut();
                            }
                            break;

                        case ConsoleKey.O:
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                game.ActivateGameTime();
                            }
                            break;

                        case ConsoleKey.P:
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                game.ActivatePowerbreak();
                            }
                            break;

                        case ConsoleKey.A:
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                Console.WriteLine("Skriv in antal sekunder att justera, sätt '-' framför siffran för att dra tillbaka klockan");
                                TimeSpan timeAdjustment = ConsoleDialogs.TimeSpanFromSeconds();
                                game.GameClock.AdjustActiveClockTime(timeAdjustment);
                            }
                            break;

                        case ConsoleKey.S:
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                Console.WriteLine("Ändra matchttid");
                                TimeSpan timeAdjustment = ConsoleDialogs.TimeSpanFromMinutesSeconds();
                                game.GameClock.SetCurrentTime(timeAdjustment);
                            }
                            break;

                        case ConsoleKey.R:
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                bool removed = false;
                                success = false;
                                while (!removed)
                                {
                                    Console.WriteLine("Radera utvisning");
                                    (team, cancel) = ConsoleDialogs.ChooseTeam();
                                    if (cancel)
                                        break;

                                    Console.WriteLine("Ange index för utvisning [i]");
                                    while (!success)
                                    {
                                        (index, success) = ConvertInput.ConvertToInt();
                                    }

                                    removed = game.GamePenalties.RemovePenaltyWithIndex(index, team);
                                }
                            }
                            break;

                        case ConsoleKey.M:
                            bool moved = false;
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                while (!moved)
                                {
                                    Console.WriteLine("Flytta utvisning till toppen");
                                    (team, cancel) = ConsoleDialogs.ChooseTeam();
                                    if (cancel)
                                        break;

                                    Console.WriteLine("Ange index för utvisning [i]");
                                    while (!success)
                                    {
                                        (index, success) = ConvertInput.ConvertToInt();
                                    }
                                    moved = game.GamePenalties.MovePenaltyToTop(index, team);
                                }
                            }
                            break;

                        case ConsoleKey.E:
                            cancel = false;
                            success = false;
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                Console.WriteLine("Ändra tid för utvisning\n");

                                (team, cancel) = ConsoleDialogs.ChooseTeam();
                                if (cancel)
                                    break;

                                Console.WriteLine("Ange index för utvisning [i]");
                                while (!success)
                                {
                                    (index, success) = ConvertInput.ConvertToInt();
                                }

                                (newPenaltyTime, cancel) = ConsoleDialogs.ChoosePenaltyTime();

                                game.GamePenalties.SetPenaltyRemainingTime(index, newPenaltyTime, team);
                            }
                            break;

                        case ConsoleKey.U:

                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                Console.WriteLine("Lägg till ny utvisning\n");

                                (team, cancel) = ConsoleDialogs.ChooseTeam();
                                if (cancel)
                                    break;

                                int playerNumber = ConsoleDialogs.SetPlayerNumber();

                                (newPenaltyTime, cancel) = ConsoleDialogs.ChoosePenaltyTime();
                                if (cancel)
                                    break;

                                game.GamePenalties.AddNewPenalty(playerNumber, newPenaltyTime, team);
                            }
                            break;


                        case ConsoleKey.I:
                            if (!game.GameClock.ActiveTimer.IsRunning)
                            {
                                game.ActivateIntermission();
                            }
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
                            Console.WriteLine("Ändra period");
                            (confirmed, cancel) = ConsoleDialogs.Confirm(false);
                            if (confirmed)
                            {
                                if (input.Modifiers.HasFlag(ConsoleModifiers.Shift))
                                {
                                    game.PreviousPeriod();
                                }
                                else
                                {
                                    game.NextPeriod();
                                }
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
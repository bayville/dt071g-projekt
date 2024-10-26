using System.Runtime.CompilerServices;

namespace Scoreboard
{
    public class ConsoleDisplay
    {

        public ConsoleDisplay(Game game)
        {
            game.UpdateGame += (sender, data) => UpdateDisplay(data);
        }
        //         public void RegisterEventHandlers(Game game)
        // {
        // }
        public void UpdateDisplay(GameEventArgs data)
        {
            Console.Clear();
            Console.WriteLine("I UpdateDisplay");
            Console.WriteLine("\nCONTROLS:\n");
            Console.WriteLine("S to start\nP to pause\nQ to quit\nA to adjust time\nN for new period\n\n");
            Console.WriteLine(FormatDisplayTime(data.GameClock));
            // Console.WriteLine($"Time passed: {data.TimePassed}");
            Console.WriteLine("\nSCORE:");
            Console.WriteLine($"{data.HomeScore} : H - A : {data.AwayScore}");


            if (!data.IsRunning)
            {
                Console.WriteLine("Klockan stoppad!");
            }
        }

        private string FormatDisplayTime(TimeSpan time)
        {
            if (time.TotalMinutes >= 1)
            {
                if (time.TotalMinutes <= 9){
                    return $"Tid:\n{time:m\\:ss}\n";
                }   
                return $"Tid:\n{time:mm\\:ss}\n";
            } 
            else
            {
                return $"Tid:\n{time:ss\\.f}\n";
            }
        }

    }
}
namespace Scoreboard
{
    public class ConsoleDisplay
    {
         public void RegisterEventHandlers(GameTimer timer) {
            timer.Refresh += (sender, e) => {
                Console.Clear();
                Console.WriteLine("Press 's' to start, 'p' to pause, 'q' to quit.");
                Console.WriteLine($"Game Clock: {e.gameClock:mm\\:ss\\:f}");
                Console.WriteLine($"Time passed: {e.timePassed}");
            };

            timer.TimeStopped += (sender, e) => {
                Console.WriteLine("Time has stopped!");
            };
        }
    }
}
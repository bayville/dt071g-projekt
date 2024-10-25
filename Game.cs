namespace Scoreboard
{
    public class Game
    {
    private readonly GameTimer gameTimer; 
    private ConsoleDisplay consoleDisplay;

    public Game(){
        Console.WriteLine("I Start");
        consoleDisplay = new();
        gameTimer = NewTimer();
    }
        public void Start()
        {
            _ = gameTimer.StartClockAsync(); 
        }

        public void Stop()
        {
            gameTimer.StopClock();
        }

        public void AdjustTime(TimeSpan timeAdjustment)
        {
            gameTimer.AdjustTime(timeAdjustment);
        }

        public GameTimer NewTimer() {
            if(gameTimer != null){
                Stop();
            }
            
            Console.WriteLine("Ny period, fyll i antal minuter");
            int minutes = Convert.ToInt32(Console.ReadLine());

            var newTimer = new GameTimer(50); // 0.05-sekundsintervall
            newTimer.SetTimers(TimeSpan.FromMinutes(minutes));
            consoleDisplay.RegisterEventHandlers(newTimer);
            return newTimer;
        }  
    }


}
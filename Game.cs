namespace Scoreboard
{
    public class Game
    {
    private GameTimer gameTimer; 
    private readonly ConsoleDisplay consoleDisplay;
    private readonly GameScore score;
    public EventHandler<GameEventArgs>? UpdateGame;
    public GameSettings Settings {get; private set;}

    public Game(GameSettings settings){
        Settings = settings;
        Console.WriteLine("I Start");
        consoleDisplay = new(this);
        score = new();
        gameTimer = NewTimer(settings.PeriodLength);

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

        public void AddGoal(int team)
        {
            score.AddGoal(team);
            Update();  // Uppdatera displayen efter ett mål
        }
        public void RemoveGoal(int team)
        {
            score.RemoveGoal(team);
            Update();  // Uppdatera displayen efter ett mål
        }


        public GameTimer NewTimer(int periodLength) {
            if(gameTimer != null){
                Stop();
            }
            
            Console.WriteLine($"Ny period, fyll i antal minuter eller tryck enter för {periodLength} ");
                  
            string input = Console.ReadLine()!;
            if (!String.IsNullOrWhiteSpace(input))
            {
                periodLength = Convert.ToInt32(input);
            }

            

            var newTimer = new GameTimer(periodLength, Settings.CountDown);
            newTimer.Refresh += (sender, args) => Update();
            gameTimer = newTimer;
            Update();
            return newTimer;
        }  

        private void Update()
        {
            // Console.WriteLine("I update");
            // Console.WriteLine($"{gameTimer.gameClock} {gameTimer.timePassed} {gameTimer.isRunning} {score.HomeScore} {score.AwayScore}");
            UpdateGame?.Invoke(this, new GameEventArgs(gameTimer.gameClock, gameTimer.timePassed, gameTimer.isRunning, score.HomeScore, score.AwayScore));
        }
    }


}
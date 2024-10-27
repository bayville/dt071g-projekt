
namespace Scoreboard
{
    public class Game
    {
    private GameClock gameClock;
    private readonly GameScore score;
    public EventHandler<GameEventArgs>? UpdateGame;
    public GameSettings Settings {get; private set;}

    public Game(GameSettings settings){
        Settings = settings;
        Console.WriteLine("I Start");
        score = new();
        gameClock = new GameClock();
        gameClock.activeTimer.TimerUpdated += (sender, args) => Update();

    }
        public void ActivateTimeOut()
        {
            gameClock.ActivateTimeOut();
            gameClock.activeTimer.TimerUpdated += (sender, args) => Update();
            Update();
        }
        public void ActivateIntermisson()
        {
            gameClock.ActivateIntermission();
            gameClock.activeTimer.TimerUpdated += (sender, args) => Update();
            Update();
        }

        public void ActivateGameTime()
        {
            gameClock.ActivateGameTime();
            Update();
        }
        public void Start()
        {
            gameClock.StartActiveClock(); 
        }

        public void Stop()
        {
            gameClock.StopActiveClock();
        }

        public void AdjustTime(TimeSpan timeAdjustment)
        {
            // gameTimer.AdjustTime(timeAdjustment);
        }

        public void AddGoal(int team)
        {
            score.AddGoal(team);
            Update();
        }
        public void RemoveGoal(int team)
        {
            score.RemoveGoal(team);
            Update();
        }


        public void NewPeriodTimer(TimeSpan periodLength) {
            // if(gameTimer != null){
            //     Stop();
            // }
            gameClock.NewPeriodTimer(periodLength);
            gameClock.activeTimer.TimerUpdated += (sender, args) => Update();
            Update();
            // return newTimer;
        }  

        private void Update()
        {
            // Console.WriteLine($"i Update: {gameClock.activeTimer.CurrentTime}");
            // UpdateGame?.Invoke(this, new GameEventArgs(gameClock.activeTimer.CurrentTime, gameClock.activeTimer.IsRunning, gameClock.activeTimer.Mode ,score.HomeScore, score.AwayScore));
            UpdateGame?.Invoke(this, new GameEventArgs(gameClock, score));
        }
    }


}
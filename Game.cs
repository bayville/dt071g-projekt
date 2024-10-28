namespace Scoreboard
{
    public class Game
    {
        private GameClock gameClock;
        private readonly GameScore gameScore;
        private readonly GamePeriod gamePeriod;
        public EventHandler<GameEventArgs>? UpdateGame;
        public GameSettings Settings { get; private set; }

        public Game(GameSettings settings)
        {
            Settings = settings;
            gameScore = new();
            gamePeriod = new(settings);
            gameClock = new GameClock(settings);

            RegisterForUpdates();
        }

        private void RegisterForUpdates()
        {
            UnregisterFromUpdates();

            gameClock.activeTimer.TimerUpdated += OnTimerUpdated;
            gameClock.activeTimer.TimerEnded += OnTimerEnded;
        }

        private void UnregisterFromUpdates()
        {
            if (gameClock.activeTimer != null)
            {
                gameClock.activeTimer.TimerUpdated -= OnTimerUpdated;
            }
        }

        private void OnTimerUpdated(object? sender, EventArgs args)
        {

            Update();
        }

        private void OnTimerEnded(object? sender, EventArgs e)
        {
            Console.WriteLine(gameClock.activeTimer.GetType());
            ActivateGameTime();
        }

        public void ActivateTimeOut()
        {
            UnregisterFromUpdates();
            gameClock.ActivateTimeOut();
            RegisterForUpdates();
            Update();
        }

        public void ActivateIntermission()
        {
            UnregisterFromUpdates();
            gameClock.ActivateIntermission();
            RegisterForUpdates();
            Update();
        }

        public void ActivateGameTime()
        {
            UnregisterFromUpdates();
            gameClock.ActivateGameTime();
            RegisterForUpdates();
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
            gameClock.AdjustActiveClockTime(timeAdjustment);
        }

        public void AddGoal(int team)
        {
            gameScore.AddGoal(team);
            Update();
        }

        public void RemoveGoal(int team)
        {
            gameScore.RemoveGoal(team);
            Update();
        }

        public void NextPeriod()
        {
            UnregisterFromUpdates();
            gameClock.StopActiveClock();

            gamePeriod.IncrementPeriod();
            var periodLength = gamePeriod.GetPeriodLength();
            gameClock.NewPeriodTimer(periodLength, Settings.CountDown);

            RegisterForUpdates();
            Update();
        }

        public void PreviousPeriod()
        {
            UnregisterFromUpdates();
            gameClock.StopActiveClock();

            gamePeriod.DecrementPeriod();
            var periodLength = gamePeriod.GetPeriodLength();
            gameClock.NewPeriodTimer(periodLength, Settings.CountDown);

            RegisterForUpdates();
            Update();
        }

        private void Update()
        {
            UpdateGame?.Invoke(this, new GameEventArgs(gameClock, gameScore, gamePeriod));
        }
    }
}

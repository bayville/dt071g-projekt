namespace Scoreboard
{
    public class Game
    {
        public GameClock GameClock { get; private set; }
        public GameScore GameScore { get; private set; }
        public GamePeriod GamePeriod { get; private set; }
        public GamePenalties GamePenalties { get; private set;}
        public ConsoleDisplay ConsoleDisplay {get; private set;}
        public EventHandler<GameEventArgs>? UpdateGame;
        public GameSettings Settings { get; private set; }

        public Game(GameSettings settings, bool isRestore, GameEventArgs? restoreData)
        {
            Settings = settings;
            GameScore = new();
            GamePeriod = new(settings);
            GamePenalties = new GamePenalties();
            GameClock = new GameClock(settings, GamePenalties);
            ConsoleDisplay = new ConsoleDisplay(this);

            // If isRestore is true, call method to restore game
            if (isRestore && restoreData != null)
            {
                RestoreGameState(restoreData);
            }

            // Listen for events
            RegisterForUpdates();
            GameScore.ScoreChanged += OnGameChanged;
            GamePeriod.PeriodChanged += OnGameChanged;
            GamePenalties.PenaltyChanged += OnGameChanged;
        }

        // Timer macros    
        public void ActivateTimeOut()
        {
            UnregisterFromUpdates();
            GameClock.ActivateTimeOut();
            RegisterForUpdates();
            Update();
        }

        public void ActivateIntermission()
        {
            UnregisterFromUpdates();
            GameClock.ActivateIntermission();
            RegisterForUpdates();
            Update();
        }
        public void ActivatePowerbreak()
        {
            UnregisterFromUpdates();
            GameClock.ActivatePowerbreak();
            RegisterForUpdates();
            Update();
        }

        public void ActivateGameTime()
        {
            UnregisterFromUpdates();
            GameClock.ActivateGameTime();
            RegisterForUpdates();
            Update();
        }


        // Periods macros
        public void NextPeriod()
        {
            GameClock.StopActiveClock();
            UnregisterFromUpdates();
            GamePeriod.IncrementPeriod();
            var periodLength = GamePeriod.GetPeriodLength();
            GameClock.NewPeriodTimer(periodLength, GamePenalties, Settings.CountDown);

            RegisterForUpdates();
            Update();
        }

        public void PreviousPeriod()
        {

            GameClock.StopActiveClock();
            UnregisterFromUpdates();

            GamePeriod.DecrementPeriod();
            var periodLength = GamePeriod.GetPeriodLength();
            GameClock.NewPeriodTimer(periodLength, GamePenalties, Settings.CountDown);

            RegisterForUpdates();
            Update();
        }

        // Restore previous game
        private void RestoreGameState(GameEventArgs restoreData)
        {
            GameScore.SetScore(restoreData.HomeScore, restoreData.AwayScore);
            GamePeriod.SetCurrentPeriod(restoreData.CurrentPeriod);
            GameClock.SetCurrentTime(restoreData.CurrentTime);
            GamePenalties.RestorePenaltyLists(restoreData.HomePenalties, restoreData.AwayPenalties);
        }

        // Events
        private void RegisterForUpdates()
        {
            UnregisterFromUpdates();

            GameClock.ActiveTimer.TimerUpdated += OnTimerUpdated;
            GameClock.ActiveTimer.TimerEnded += OnTimerEnded;
        }

        private void UnregisterFromUpdates()
        {
            if (GameClock.ActiveTimer != null)
            {
                GameClock.ActiveTimer.TimerUpdated -= OnTimerUpdated;
            }
        }

        private void OnTimerUpdated(object? sender, EventArgs args)
        {
            Update();
        }


        private void OnTimerEnded(object? sender, EventArgs e)
        {
            Console.WriteLine(GameClock.ActiveTimer.GetType());
            // --- Play GameHorn --- (Implement class) 
            ActivateGameTime();
        }

        private void OnGameChanged(object? sender, EventArgs e)
        {
            Update();
        }

        public GameEventArgs Update()
        {
            GameEventArgs gameEventArgs = new GameEventArgs(GameClock.ActiveTimer.CurrentTime, GameClock.ActiveTimer.Mode, GameClock.ActiveTimer.IsRunning, GamePenalties._homePenalties, GamePenalties._awayPenalties, GameScore.HomeScore, GameScore.AwayScore, GamePeriod.CurrentPeriod, GamePeriod.IsOvertime, Settings);
            UpdateGame?.Invoke(this, gameEventArgs);
            return gameEventArgs;
        }
    }
}

namespace Scoreboard
{
    public class Game
    {
        private GameClock gameClock;
        private readonly GameScore gameScore;
        private readonly GamePeriod gamePeriod;
        public readonly GamePenalties gamePenalties;
        public EventHandler<GameEventArgs>? UpdateGame;
        public GameSettings Settings { get; private set; }

        public Game(GameSettings settings, bool isRestore, GameEventArgs? restoreData)
        {
            Settings = settings;
            gameScore = new();
            gamePeriod = new(settings);
            gamePenalties = new GamePenalties();
            gameClock = new GameClock(settings, gamePenalties);
            
            if (isRestore && restoreData != null)
            {
                RestoreGameState(restoreData);
            }

            RegisterForUpdates();
        }

        private void RegisterForUpdates()
        {
            UnregisterFromUpdates();

            gameClock.ActiveTimer.TimerUpdated += OnTimerUpdated;
            gameClock.ActiveTimer.TimerEnded += OnTimerEnded;
        }

        private void UnregisterFromUpdates()
        {
            if (gameClock.ActiveTimer != null)
            {
                gameClock.ActiveTimer.TimerUpdated -= OnTimerUpdated;
            }
        }

        private void OnTimerUpdated(object? sender, EventArgs args)
        {
            Update();
        }

        private void OnTimerEnded(object? sender, EventArgs e)
        {
            Console.WriteLine(gameClock.ActiveTimer.GetType());
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
            Update();
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

            gameClock.StopActiveClock();
            UnregisterFromUpdates();
            gamePeriod.IncrementPeriod();
            var periodLength = gamePeriod.GetPeriodLength();
            gameClock.NewPeriodTimer(periodLength, gamePenalties, Settings.CountDown);

            RegisterForUpdates();
            Update();
        }

        public void PreviousPeriod()
        {

            gameClock.StopActiveClock();
            UnregisterFromUpdates();

            gamePeriod.DecrementPeriod();
            var periodLength = gamePeriod.GetPeriodLength();
            gameClock.NewPeriodTimer(periodLength, gamePenalties, Settings.CountDown);

            RegisterForUpdates();
            Update();
        }

        private void SetScore(int homeScore, int awayScore)
        {
            gameScore.SetScore(homeScore, awayScore);
        }

        private void SetCurrentPeriod(int currentPeriod)
        {
            gamePeriod.SetCurrentPeriod(currentPeriod);
        }

        private void SetCurrentTime(TimeSpan currentTime)
        {
            gameClock.SetCurrentTime(currentTime);
        }


        public void AddNewPenalty(int team)
        {
            Random rnd = new Random();
            int num = rnd.Next(99);
            gamePenalties.AddNewPenalty(num, TimeSpan.FromMinutes(2), team);
            Update();
        }

        public void SetPenaltyRemainingTime()
        {
            gamePenalties.SetPenaltyRemainingTime(0, 0, TimeSpan.Zero);
            Update();
        }

        public void RemoveFinishedPenalties()
        {
            gamePenalties.Update();
            Update();
        }
        private void RestoreGameState(GameEventArgs restoreData)
        {
            SetScore(restoreData.HomeScore, restoreData.AwayScore);
            SetCurrentPeriod(restoreData.CurrentPeriod);
            SetCurrentTime(restoreData.CurrentTime);
            gamePenalties.RestorePenaltyLists(restoreData.HomePenalties, restoreData.AwayPenalties);
        }

        public void Update()
        {
            UpdateGame?.Invoke(this, new GameEventArgs(gameClock.ActiveTimer.CurrentTime, gameClock.ActiveTimer.Mode, gameClock.ActiveTimer.IsRunning, gamePenalties._homePenalties, gamePenalties._awayPenalties, gameScore.HomeScore, gameScore.AwayScore, gamePeriod.CurrentPeriod, gamePeriod.IsOvertime, Settings));
        }
    }
}

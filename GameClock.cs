namespace Scoreboard
{
    public class GameClock
    {
        public GameTimer GameTimer {get; private set;}
        private GameSettings _gameSettings;
        public BaseTimer ActiveTimer {get; private set;}

        public GameClock(GameSettings settings, GamePenalties gamePenalties)
        {
            GameTimer = new GameTimer(settings.PeriodLength, gamePenalties, settings.CountDown);
            ActiveTimer = GameTimer;
            _gameSettings = settings;
        }

        // Activates timeout
        public void ActivateTimeOut()
        {
            StopActiveClock();
            ActiveTimer = new TimeOutTimer(_gameSettings.TimeOutLength);
        }

        // Activates intermission
        public void ActivateIntermission()
        {
            StopActiveClock();
            ActiveTimer = new IntermissionTimer(_gameSettings.IntermissionLength);
        }

        // Activates gametime
        public void ActivateGameTime()
        {
            StopActiveClock();
            ActiveTimer = GameTimer;
        }

        // Activates powerbreak
        public void ActivatePowerbreak()
        {
            StopActiveClock();
            ActiveTimer = new PowerbreakTimer(_gameSettings.PowerbreakLength);
        }

        // Creates a new gametimer for a new period
        public void NewPeriodTimer(TimeSpan periodLength, GamePenalties gamePenalites, bool countDown)
        {
            ActiveTimer = new GameTimer(periodLength, gamePenalites, countDown);
        }

        // Sets timer to a specif time
        public void SetCurrentTime(TimeSpan currentTime)
        {
            ActiveTimer.SetCurrentTime(currentTime);
        }

        // Starts active timer
        public void StartActiveClock()
        {
            _ = ActiveTimer.StartClockAsync();
        }

        // Stops active timer
        public void StopActiveClock()
        {
            ActiveTimer.Stop();
        }

        // Adjust active timer currenttime
        public void AdjustActiveClockTime(TimeSpan timeAdjustment)
        {
            ActiveTimer.AdjustTime(timeAdjustment);
        }
    }


}
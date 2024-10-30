namespace Scoreboard
{
    public class GameClock
    {
        private GameTimer _gameTimer;
        private GameSettings _gameSettings;
        public BaseTimer ActiveTimer {get; private set;}

        public GameClock(GameSettings settings, GamePenalties gamePenalties)
        {
            _gameTimer = new GameTimer(settings.PeriodLength, gamePenalties, settings.CountDown);
            ActiveTimer = _gameTimer;
            _gameSettings = settings;
        }
        public void ActivateTimeOut()
        {
            StopActiveClock();
            ActiveTimer = new TimeOutTimer(_gameSettings.TimeOutLength);
        }

        public void ActivateIntermission()
        {
            StopActiveClock();
            ActiveTimer = new IntermissionTimer(_gameSettings.IntermissionLength);
        }

        public void ActivateGameTime()
        {
            StopActiveClock();
            ActiveTimer = _gameTimer;
        }
        public void ActivatePowerbreak()
        {
            StopActiveClock();
            ActiveTimer = new PowerbreakTimer(_gameSettings.PowerbreakLength);
        }

        public void NewPeriodTimer(TimeSpan periodLength, GamePenalties gamePenalites, bool countDown)
        {
            ActiveTimer = new GameTimer(periodLength, gamePenalites, countDown);
        }

        public void SetCurrentTime(TimeSpan currentTime)
        {
            ActiveTimer.SetCurrentTime(currentTime);
        }
   
        
        public void StartActiveClock()
        {
            _ = ActiveTimer.StartClockAsync();
        }

        public void StopActiveClock()
        {
            ActiveTimer.Stop();
        }

        public void AdjustActiveClockTime(TimeSpan timeAdjustment)
        {
            ActiveTimer.AdjustTime(timeAdjustment);
        }
    }


}
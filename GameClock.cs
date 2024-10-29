namespace Scoreboard
{
    public class GameClock
    {
        private GameTimer _gameTimer;
        private GamePenalties _gamePenalties;
        public BaseTimer ActiveTimer {get; private set;}

        public GameClock(GameSettings settings, GamePenalties gamePenalties)
        {
            _gameTimer = new GameTimer(settings.PeriodLength, gamePenalties, settings.CountDown);
            ActiveTimer = _gameTimer;
            _gamePenalties = gamePenalties;
        }
        public void ActivateTimeOut()
        {
            StopActiveClock();
            ActiveTimer = new TimeOutTimer(TimeSpan.FromSeconds(30));
        }

        public void ActivateIntermission()
        {
            StopActiveClock();
            ActiveTimer = new IntermissionTimer(TimeSpan.FromMinutes(18));
        }

        public void ActivateGameTime()
        {
            StopActiveClock();
            ActiveTimer = _gameTimer;
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
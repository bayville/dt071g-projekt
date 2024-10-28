namespace Scoreboard
{
    public class GameClock
    {
        private GameTimer gameTimer;
        public BaseTimer activeTimer {get; private set;}
        private readonly GameSettings Settings;
        public GameClock(GameSettings settings)
        {
            Settings = settings;
            gameTimer = new GameTimer(settings.PeriodLength, settings.CountDown);
            activeTimer = gameTimer;
        }
        public void ActivateTimeOut()
        {
            StopActiveClock();
            activeTimer = new TimeOutTimer(TimeSpan.FromSeconds(30));
        }

        public void ActivateIntermission()
        {
            StopActiveClock();
            activeTimer = new IntermissionTimer(TimeSpan.FromMinutes(18));
        }

        public void ActivateGameTime()
        {
            StopActiveClock();
            activeTimer = gameTimer;
        }

        public void NewPeriodTimer(TimeSpan periodLength, bool countDown)
        {
            activeTimer = new GameTimer(periodLength, countDown);
        }
   
        
        public void StartActiveClock()
        {
            _ = activeTimer.StartClockAsync();
        }

        public void StopActiveClock()
        {
            activeTimer.Stop();
        }

        public void AdjustActiveClockTime(TimeSpan timeAdjustment)
        {
            activeTimer.AdjustTime(timeAdjustment);
        }
    }


}
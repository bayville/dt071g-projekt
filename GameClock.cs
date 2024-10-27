
namespace Scoreboard
{
    public class GameClock
    {
        private GameTimer gameTimer;
        public BaseTimer activeTimer;

        public GameClock()
        {
            gameTimer = new GameTimer(TimeSpan.FromMinutes(20), true);
            activeTimer = gameTimer;
            RegisterTimerEvents();
        }


        private void RegisterTimerEvents()
        {
            activeTimer.TimerStopped += OnTimerStopped;
            activeTimer.TimerUpdated += OnTimerUpdated;
        }

        public void ActivateTimeOut()
        {
            StopActiveClock();
            activeTimer = new TimeOutTimer(TimeSpan.FromSeconds(30));
            activeTimer.TimerUpdated += TimeEnded;
        }

        public void ActivateIntermission()
        {
            StopActiveClock();
            activeTimer = new IntermissionTimer(TimeSpan.FromMinutes(18));
            activeTimer.TimerUpdated += TimeEnded;
        }

        public void ActivateGameTime()
        {
            StopActiveClock();
            activeTimer = gameTimer;
        }

        public void NewPeriodTimer(TimeSpan periodLength)
        {
            activeTimer = new GameTimer(periodLength, true);
        }
        public void TimeEnded(object sender, EventArgs e)
        {
            if (activeTimer.CurrentTime <= TimeSpan.Zero)
            {
                StopActiveClock();
                activeTimer = gameTimer;
            };
        }
        
        public void StartActiveClock()
        {
            _ = activeTimer.StartClockAsync();
        }

        public void StopActiveClock()
        {
            activeTimer.Stop();
        }
        private void OnTimerStopped(object sender, EventArgs e)
        {
            Console.WriteLine("TimerStopped");
        }

        private void OnTimerUpdated(object sender, EventArgs e)
        {

        }
    }


}
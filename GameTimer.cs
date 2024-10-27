namespace Scoreboard
{
    public class GameTimer : BaseTimer
    {
        public TimeSpan PeriodLength { get; private set; }
        public int CurrentPeriod { get; private set; }
        public GameTimer(TimeSpan periodLength, bool countDown)
            :base(countDown, periodLength, 50)
        {
            PeriodLength = periodLength;
            CurrentTime = countDown ? periodLength : TimeSpan.Zero;
            Mode = "Game Time";
        }
    }
}
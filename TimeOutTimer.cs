namespace Scoreboard
{
    public class TimeOutTimer : BaseTimer
    {
        public int CurrentPeriod { get; private set; }
        public TimeOutTimer(TimeSpan timerLength)
            :base(true, timerLength, 100)
        {
            CurrentTime = timerLength;
            Mode = "TimeOut";
        }
    }
}
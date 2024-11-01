namespace Scoreboard
{
    public class PowerbreakTimer : BaseTimer
    {
        public int CurrentPeriod { get; private set; }
        public PowerbreakTimer(TimeSpan timerLength)
            :base(true, timerLength, 100)
        {
            CurrentTime = timerLength;
            Mode = "Powerbreak";
        }
    }
}
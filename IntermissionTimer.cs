namespace Scoreboard
{
    public class IntermissionTimer : BaseTimer
    {
        public int CurrentPeriod { get; private set; }
        public IntermissionTimer(TimeSpan timerLength)
            :base(true, timerLength, 50)
        {
            CurrentTime = timerLength;
            Mode = "Intermission";
        }
    }
}
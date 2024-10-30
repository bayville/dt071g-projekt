namespace Scoreboard
{
    public class GameSettings
    {
        public TimeSpan PeriodLength { get; private set; }
        public TimeSpan OvertimePeriodLength {get; private set;}
        public TimeSpan TimeOutLength {get; private set;}
        public TimeSpan PowerbreakLength {get; private set;}
        public TimeSpan IntermissionLength {get; private set;}
        public int NumberOfPeriods { get; private set; }
        public bool CountDown {get; private set;}

        public GameSettings(TimeSpan periodLength, TimeSpan intermissionLength,  TimeSpan overtimePeriodLength, TimeSpan timeOutLength, TimeSpan powerBreakLength, int numberOfPeriods, bool countDown)
        {
            PeriodLength = periodLength;
            IntermissionLength = intermissionLength;
            NumberOfPeriods = numberOfPeriods;
            OvertimePeriodLength = overtimePeriodLength;
            TimeOutLength = timeOutLength;
            PowerbreakLength = powerBreakLength;
            CountDown = countDown;
        }

    }

}
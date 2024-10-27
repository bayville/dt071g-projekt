namespace Scoreboard
{
    public class GameSettings
    {
        public TimeSpan PeriodLength { get; private set; }
        public int NumberOfPeriods { get; private set; }
        public int OTPeriodLength {get; private set;}
        public bool CountDown {get; private set;}
        
        public GameSettings(TimeSpan periodLength, int numberOfPeriods, int OTPeriodLength, bool countDown)
        {
            PeriodLength = periodLength;
            NumberOfPeriods = numberOfPeriods;
            this.OTPeriodLength = OTPeriodLength;
            CountDown = countDown;
        }
    }

    
}
namespace Scoreboard
{
    public class GameSettings
    {
        public int PeriodLength { get; private set; }
        public int NumberOfPeriods { get; private set; }
        public int OTPeriodLength {get; private set;}
        public bool CountDown {get; private set;}
        
        public GameSettings(int periodLength, int numberOfPeriods, int OTPeriodLength, bool countDown)
        {
            PeriodLength = periodLength;
            NumberOfPeriods = numberOfPeriods;
            this.OTPeriodLength = OTPeriodLength;
            CountDown = countDown;
        }
    }

    
}
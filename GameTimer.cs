namespace Scoreboard
{
    public class GameTimer : BaseTimer
    {
        public TimeSpan PeriodLength { get; private set; }
        public int CurrentPeriod { get; private set; }
        private GamePenalties _penalties;
        public Penalty HomePenalty1;
        public Penalty HomePenalty2;
        public Penalty AwayPenalty1;
        public Penalty AwayPenalty2;
        public GameTimer(TimeSpan periodLength, GamePenalties gamePenalties, bool countDown)
            : base(countDown, periodLength, 50)
        {
            PeriodLength = periodLength;
            CurrentTime = countDown ? periodLength : TimeSpan.Zero;
            _penalties = gamePenalties;
            Mode = "Game Time";
            HomePenalty1 = _penalties.HomePenalty1;
            HomePenalty2 = _penalties.HomePenalty2;
            AwayPenalty1 = _penalties.AwayPenalty1;
            AwayPenalty2 = _penalties.AwayPenalty2;
        }

        public override void AdjustTime(TimeSpan adjustment)
        {
            // If countdown - adds to timespan (winds back clock)
            if (CountDown)
            {
                CurrentTime -= adjustment;

                // Prevents current time to be greater then period length
                if (CurrentTime > PeriodLength)
                {
                    CurrentTime = PeriodLength;
                }

            }
            else
            {  
                // If countup - prevents adjustment to give period negative time
                if ((CurrentTime += adjustment) < TimeSpan.Zero)
                {
                    adjustment -= CurrentTime;
                    CurrentTime = TimeSpan.Zero;
                }
                else
                {   
                    CurrentTime += adjustment;
                }
            }

            
            HomePenalty1 = _penalties.HomePenalty1;
            HomePenalty2 = _penalties.HomePenalty2;
            AwayPenalty1 = _penalties.AwayPenalty1;
            AwayPenalty2 = _penalties.AwayPenalty2;

            AdjustPenaltyTimers(HomePenalty1, HomePenalty2, adjustment);
            AdjustPenaltyTimers(AwayPenalty1, AwayPenalty2, adjustment);

            OnTimerUpdated();
        }



        protected override void UpdateTime()
        {
            base.UpdateTime();

            HomePenalty1 = _penalties.HomePenalty1;
            HomePenalty2 = _penalties.HomePenalty2;
            AwayPenalty1 = _penalties.AwayPenalty1;
            AwayPenalty2 = _penalties.AwayPenalty2;
            
            AdjustPenaltyTimers(HomePenalty1, HomePenalty2, TimeElapsed);
            AdjustPenaltyTimers(AwayPenalty1, AwayPenalty2, TimeElapsed);

            _penalties.Update();

        }

        private void AdjustPenaltyTimers(Penalty penalty1, Penalty penalty2, TimeSpan adjustment)
        {

            if (penalty1.RemainingTime > TimeSpan.Zero)
            {
                penalty1.RemainingTime -= adjustment;
                if (penalty1.RemainingTime > penalty1.PenaltyLength)
                {
                    penalty1.RemainingTime = penalty1.PenaltyLength;
                }
            }


            if (penalty2.RemainingTime > TimeSpan.Zero)
            {
                penalty2.RemainingTime -= adjustment;
                if (penalty2.RemainingTime > penalty2.PenaltyLength)
                {
                    penalty2.RemainingTime = penalty2.PenaltyLength;
                }
            }
        }
    }
}
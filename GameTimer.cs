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


        // Overrides method in BaseTimer to add ability adjust penalties.
        public override void AdjustTime(TimeSpan adjustment)
        {
            base.AdjustTime(adjustment);

            
            HomePenalty1 = _penalties.HomePenalty1;
            HomePenalty2 = _penalties.HomePenalty2;
            AwayPenalty1 = _penalties.AwayPenalty1;
            AwayPenalty2 = _penalties.AwayPenalty2;

            // Adjust time on penaltytimers.
            AdjustPenaltyTimers(HomePenalty1, HomePenalty2, adjustment);
            AdjustPenaltyTimers(AwayPenalty1, AwayPenalty2, adjustment);

            OnTimerUpdated();
        }


        // Overrides method in BaseTimer to add ability adjust penalties
        protected override void UpdateTime()
        {
            base.UpdateTime();

            HomePenalty1 = _penalties.HomePenalty1;
            HomePenalty2 = _penalties.HomePenalty2;
            AwayPenalty1 = _penalties.AwayPenalty1;
            AwayPenalty2 = _penalties.AwayPenalty2;
            
            // Adjust time on penaltytimers.
            AdjustPenaltyTimers(HomePenalty1, HomePenalty2, TimeElapsed);
            AdjustPenaltyTimers(AwayPenalty1, AwayPenalty2, TimeElapsed);

            // Calls update function in penalty-class
            _penalties.Update();

        }


        // Adjust penalty timers
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
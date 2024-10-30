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


        protected override void UpdateTime()
        {
            base.UpdateTime();

            HomePenalty1 = _penalties.HomePenalty1;
            HomePenalty2 = _penalties.HomePenalty2;
            AwayPenalty1 = _penalties.AwayPenalty1;
            AwayPenalty2 = _penalties.AwayPenalty2;
            AdjustPenaltyTimers(HomePenalty1, HomePenalty2);
            AdjustPenaltyTimers(AwayPenalty1, AwayPenalty2);

            _penalties.Update();

        }

        private void AdjustPenaltyTimers(Penalty penalty1, Penalty penalty2)
        {
            if (penalty1.RemainingTime > TimeSpan.Zero)
            {
                penalty1.RemainingTime -= TimeElapsed;
            }


            if (penalty2.RemainingTime > TimeSpan.Zero)
            {
                penalty2.RemainingTime -= TimeElapsed;
            }
        }
    }
}
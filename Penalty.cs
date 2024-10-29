namespace Scoreboard
{
    public class Penalty
    {
        public int PlayerNumber { get; set; }
        public TimeSpan PenaltyLength { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public bool IsActive { get; set; }
        public int Team { get; set; }
        public DateTime TimeInitiated {get; private set;}

        public Penalty(int playerNumber, TimeSpan penaltyLength, TimeSpan remainingTime, int team)
        {
            PlayerNumber = playerNumber;
            PenaltyLength = penaltyLength;
            RemainingTime = remainingTime;
            IsActive = false;
            Team = team;
            TimeInitiated = DateTime.Now;
        }
    }


}
namespace Scoreboard
{
    public class GameEventArgs : EventArgs
    {
        public TimeSpan GameClock { get; set; }
        public TimeSpan TimePassed { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool IsRunning { get; set; }
        
        public GameEventArgs(TimeSpan gameClock, TimeSpan timePassed, bool isRunning, int homeScore, int awayScore)
        {
            GameClock = gameClock;
            TimePassed = timePassed;
            IsRunning = isRunning;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }
    }
}

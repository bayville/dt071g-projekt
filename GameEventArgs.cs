namespace Scoreboard
{
    public class GameEventArgs : EventArgs
    {
        // public TimeSpan GameClock { get; set; }
        public TimeSpan TimePassed { get; private set; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; set; }
        public bool IsRunning { get; set; }
        public GameClock GameClock {get; set;}
        public GameScore GameScore {get; set;}
        // public string Mode { get; set;}
        // public GameEventArgs(TimeSpan gameClock, bool isRunning, string mode, int homeScore, int awayScore)
        // {
        //     GameClock = gameClock;
        //     IsRunning = isRunning;
        //     HomeScore = homeScore;
        //     AwayScore = awayScore;
        //     Mode = mode;
        // }
        
        public GameEventArgs(GameClock gameClock, GameScore gameScore)
        {
            GameClock = gameClock;
            GameScore = gameScore;
        }
    }
}

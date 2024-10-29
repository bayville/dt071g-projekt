namespace Scoreboard
{
    public class GameEventArgs : EventArgs
    {
        public TimeSpan CurrentTime {get; set;}
        public string GameMode {get; set;}
        public bool IsRunning {get; set;}
        public bool IsOvertime {get; set;}
        public int CurrentPeriod {get; set;}
        public int AwayScore {get; set;}
        public int HomeScore {get; set;}
        public GameSettings GameSettings {get; set;}
        
        public GameEventArgs(TimeSpan currentTime, string gameMode, bool isRunning, int homeScore, int awayScore, int currentPeriod, bool isOvertime, GameSettings gameSettings)
        {
            CurrentTime = currentTime;
            GameMode = gameMode;
            IsRunning = isRunning;
            HomeScore = homeScore;
            AwayScore = awayScore;
            CurrentPeriod = currentPeriod;
            IsOvertime = isOvertime;
            GameSettings = gameSettings;
        }
    }
}

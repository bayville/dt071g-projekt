namespace Scoreboard
{
    public class ScoreEventArgs : EventArgs
{
    public int HomeScore { get; }
    public int AwayScore { get; }

    public ScoreEventArgs(int homeScore, int awayScore)
    {
        HomeScore = homeScore;
        AwayScore = awayScore;
    }
}
}
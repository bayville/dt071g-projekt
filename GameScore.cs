namespace Scoreboard
{
    public class GameScore
    {
        public int HomeScore {get; private set;}
        public int AwayScore {get; private set;}
        public EventHandler? ScoreChanged;
    
    // Adds a goal for specific team
    public void AddGoal(int team)
    {
        if (team == 0)
        {
            HomeScore++;
        }
        else if (team == 1)
        {
            AwayScore++;
        }
        UpdateScore();
    }

    // Removes a goal for specific team
     public void RemoveGoal(int team)
    {
        if (team == 0)
        {
             HomeScore = Math.Max(0, HomeScore - 1);
        }
        else if (team == 1)
        {
             AwayScore = Math.Max(0, AwayScore - 1);
        }
        UpdateScore();
    }

    // Uses setScore-method to reset score.
    public void ResetScore()
    {
        SetScore(0, 0);
    }

    // Method to set score for both home and away team
    public void SetScore(int homeScore, int awayScore)
    {
        HomeScore = homeScore;
        AwayScore = awayScore;
        UpdateScore();
    }

    // Triggers event
    public void UpdateScore()
    {
        ScoreChanged?.Invoke(this, new ScoreEventArgs(HomeScore, AwayScore));
    }


    }
}
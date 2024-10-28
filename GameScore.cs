namespace Scoreboard
{
    public class GameScore
    {
        public int HomeScore {get; private set;}
        public int AwayScore {get; private set;}
    

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
    }

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
    }

    }
}
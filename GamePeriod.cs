using Scoreboard;

public class GamePeriod
{
    public int NumberOfPeriods { get; private set; }
    public int CurrentPeriod { get; private set; }
    public bool IsOvertime { get; private set; }
    private TimeSpan regularPeriodLength;
    private TimeSpan overtimePeriodLength;

    public GamePeriod(GameSettings settings)
    {
        NumberOfPeriods = settings.NumberOfPeriods;
        CurrentPeriod = 1;
        IsOvertime = false;
        regularPeriodLength = settings.PeriodLength;
        overtimePeriodLength = settings.OvertimePeriodLength;
    }

    public void IncrementPeriod()
    {
        CurrentPeriod++;
        CheckIfOvertime();
    }

    public void DecrementPeriod()
    {
        CurrentPeriod = Math.Max(0, CurrentPeriod - 1);
        CheckIfOvertime();
    }

    public TimeSpan GetPeriodLength()
    {
        return IsOvertime ? overtimePeriodLength : regularPeriodLength;
    }

    private void CheckIfOvertime()
    {
        IsOvertime = CurrentPeriod > NumberOfPeriods;
    }
}

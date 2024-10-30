using Scoreboard;

public class GamePeriod
{
    public int NumberOfPeriods { get; private set; }
    public int CurrentPeriod { get; private set; }
    public bool IsOvertime { get; private set; }
    private TimeSpan _regularPeriodLength;
    private TimeSpan _overtimePeriodLength;
    public EventHandler? PeriodChanged;

    public GamePeriod(GameSettings settings)
    {
        NumberOfPeriods = settings.NumberOfPeriods;
        CurrentPeriod = 1;
        IsOvertime = false;
        _regularPeriodLength = settings.PeriodLength;
        _overtimePeriodLength = settings.OvertimePeriodLength;
    }

    public void IncrementPeriod()
    {
        CurrentPeriod++;
        CheckIfOvertime();
    }

    public void DecrementPeriod()
    {
        CurrentPeriod = Math.Max(1, CurrentPeriod - 1);
        CheckIfOvertime();
    }

    public TimeSpan GetPeriodLength()
    {
        return IsOvertime ? _overtimePeriodLength : _regularPeriodLength;
    }

    public void SetCurrentPeriod(int currentPeriod)
    {
        CurrentPeriod = currentPeriod;
        CheckIfOvertime();
    }

    private void CheckIfOvertime()
    {
        IsOvertime = CurrentPeriod > NumberOfPeriods;
        Update();
    }

    public void Update()
    {
        PeriodChanged?.Invoke(this, new EventArgs());
    }
}

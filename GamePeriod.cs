using Scoreboard;

public class GamePeriod
{
    public int NumberOfPeriods { get; private set; }
    public int CurrentPeriod { get; private set; }
    public bool IsOvertime { get; private set; }
    private TimeSpan _regularPeriodLength;
    private TimeSpan _overtimePeriodLength;

    // Event that triggers when period data has changed
    public EventHandler? PeriodChanged;

    public GamePeriod(GameSettings settings)
    {
        NumberOfPeriods = settings.NumberOfPeriods;
        CurrentPeriod = 1;
        IsOvertime = false;
        _regularPeriodLength = settings.PeriodLength;
        _overtimePeriodLength = settings.OvertimePeriodLength;
    }

    // Increments period
    public void IncrementPeriod()
    {
        CurrentPeriod++;
        CheckIfOvertime();
    }

    // Decrements period and makes shure it does not go below 1
    public void DecrementPeriod()
    {
        CurrentPeriod = Math.Max(1, CurrentPeriod - 1);
        CheckIfOvertime();
    }

    // Gets period length based on if its overtime or not
    public TimeSpan GetPeriodLength()
    {
        return IsOvertime ? _overtimePeriodLength : _regularPeriodLength;
    }

    // Sets current period to a specifik period
    public void SetCurrentPeriod(int currentPeriod)
    {
        CurrentPeriod = currentPeriod;
        CheckIfOvertime();
    }

    // Checks if it overtime.
    private void CheckIfOvertime()
    {
        IsOvertime = CurrentPeriod > NumberOfPeriods;
        Update();
    }

    // Triggers event
    public void Update()
    {
        PeriodChanged?.Invoke(this, new EventArgs());
    }
}

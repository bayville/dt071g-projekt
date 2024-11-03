namespace Scoreboard
{
    public abstract class BaseTimer
    {
        public TimeSpan CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }
        public TimeSpan TimerLength { get; private set; }
        public string Mode { get; set; } = "Game Time";
        protected DateTime startTime;
        public TimeSpan TimeElapsed { get; private set; }
        private readonly int Interval;
        private CancellationTokenSource? cancellationTokenSource;
        public bool CountDown { get; private set; }
        public event EventHandler? TimerUpdated;
        public event EventHandler? TimerStopped;
        public event EventHandler? TimerEnded;

        protected BaseTimer(bool countDown, TimeSpan timerLength, int interval)
        {
            CountDown = countDown;  // Sets mode to countdown or countup
            Interval = interval; // Determines how often timer should be updated.
            TimerLength = timerLength; // Sets timerlength
        }

        // runs a task that runs the timer
        public async Task StartClockAsync()
        {
            if (IsRunning) return;

            IsRunning = true;
            cancellationTokenSource = new CancellationTokenSource();
            startTime = DateTime.Now; // sets timestamp

            while (IsRunning)
            {
                await Task.Delay(Interval, cancellationTokenSource.Token);

                UpdateTime();
                OnTimerUpdated();

                if (ShouldStop())
                {

                    Stop();
                    OnTimerEnded();
                }
            }
        }

        // Stops the timer
        public void Stop()
        {
            IsRunning = false;
            cancellationTokenSource?.Cancel();

            // Trigger event for time updated
            OnTimerUpdated();
        }

        // Adjust timer currenttime with timespan
        public virtual void AdjustTime(TimeSpan adjustment)
        {
            // If countdown - adds to timespan (winds back clock)
            if (CountDown)
            {
                CurrentTime -= adjustment;

                // Prevents current time to be greater then timer length
                if (CurrentTime > TimerLength)
                {
                    CurrentTime = TimerLength;
                }

            }
            else
            {
                // If countup - prevents adjustment to give timer negative time
                if ((CurrentTime += adjustment) < TimeSpan.Zero)
                {
                    adjustment -= CurrentTime;
                    CurrentTime = TimeSpan.Zero;
                }
                else
                {
                    CurrentTime += adjustment;

                }
            }

            // Trigger event for time updated
            OnTimerUpdated();
        }

        // Set timer currentTime to exact time
        public void SetCurrentTime(TimeSpan currentTime)
        {
            CurrentTime = currentTime;
            if (CurrentTime > TimerLength)
            {
                CurrentTime = TimerLength;
            }
            else if (CurrentTime <= TimeSpan.Zero)
            {
                CurrentTime = TimeSpan.Zero;
            }

            // Trigger event for time updated
            OnTimerUpdated();
        }

        // Updates timer CurrentTime
        protected virtual void UpdateTime()
        {
            var now = DateTime.Now;         // Gets timestamp
            TimeElapsed = now - startTime;  // Gets time elapsed subtracting now with starttime

            // Adjust so TimeElapsed is not greater than remaining timer on timer
            if (CountDown && CurrentTime - TimeElapsed < TimeSpan.Zero)
            {
                TimeElapsed = CurrentTime; // Sets time elapsed to exact time to reach 00:00
            }
            else if (!CountDown && CurrentTime + TimeElapsed > TimerLength)
            {
                TimeElapsed = TimerLength - CurrentTime; // Sets time elapsed to exact time to reach maximum timer length
            }

            startTime = now; // Sets starttime to now

            // Adjust CurrentTime depending on if timer is countdown or countup
            CurrentTime = CountDown ? CurrentTime - TimeElapsed : CurrentTime + TimeElapsed;
        }

        // Determines if timer has reached end
        protected virtual bool ShouldStop()
        {
            return CountDown ? CurrentTime <= TimeSpan.Zero : CurrentTime >= TimerLength;
        }

        // Triggers event
        protected virtual void OnTimerUpdated()
        {
            TimerUpdated?.Invoke(this, new TimerEventArgs(this));
        }

        // Triggers event
        protected virtual void OnTimerStopped()
        {
            TimerStopped?.Invoke(this, new TimerEventArgs(this));
        }
        // Triggers event
        protected virtual void OnTimerEnded()
        {
            TimerEnded?.Invoke(this, new TimerEventArgs(this));
        }


    }
}
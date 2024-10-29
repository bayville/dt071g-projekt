namespace Scoreboard
{
    public abstract class BaseTimer
    {
        public TimeSpan CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }
        public TimeSpan TimerLength { get; private set; }
        public string Mode {get; set;} = "Game Time";
        protected DateTime startTime;
        public TimeSpan TimeElapsed { get; private set; }
        private readonly int Interval;
        private CancellationTokenSource? cancellationTokenSource;
        public bool CountDown {get; private set;}
        public event EventHandler? TimerUpdated;
        public event EventHandler? TimerStopped;
        public event EventHandler? TimerEnded;


        protected BaseTimer( bool countDown, TimeSpan timerLength, int interval)
        {
            CountDown = countDown;
            Interval = interval;
            TimerLength = timerLength;
        }

        public async Task StartClockAsync()
        {
            if (IsRunning) return;
            
            IsRunning = true;
            cancellationTokenSource = new CancellationTokenSource();
            startTime = DateTime.Now;

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

        public void Stop()
        {
            IsRunning = false;
            cancellationTokenSource?.Cancel();
            OnTimerStopped();
        }

        public void AdjustTime(TimeSpan adjustment)
        {
            CurrentTime += adjustment;
            OnTimerUpdated();
        }

        public void SetCurrentTime(TimeSpan currentTime)
        {
            CurrentTime = currentTime;
            OnTimerUpdated();
        }

        protected virtual void UpdateTime()
        {
            var now = DateTime.Now;
            TimeElapsed = now - startTime;
            startTime = now;


            CurrentTime = CountDown ? CurrentTime - TimeElapsed : CurrentTime + TimeElapsed;
        }

        protected virtual bool ShouldStop()
        {
            return CountDown ? CurrentTime <= TimeSpan.Zero : CurrentTime >= TimerLength;
        }

        protected virtual void OnTimerUpdated()
        {
            TimerUpdated?.Invoke(this, new TimerEventArgs(this));
        }

        protected virtual void OnTimerStopped()
        {
            TimerStopped?.Invoke(this, new TimerEventArgs(this));
        }

        protected virtual void OnTimerEnded()
        {
            TimerEnded?.Invoke(this, new TimerEventArgs(this));
        }


    }
}
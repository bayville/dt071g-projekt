namespace Scoreboard
{
    public class GameTimer
    {

        public TimeSpan gameClock { get; private set; }
        public bool isRunning { get; private set; }
        public TimeSpan timePassed { get; private set; } // For testing
        private readonly int interval = 50; // Time in milliseconds
        private CancellationTokenSource? cancellationTokenSource;
        private DateTime startTime;
        private DateTime currentTime;
        public TimeSpan periodLength {get; private set;}
        private readonly bool countDown;

        // Events
        public delegate void RefreshHandler(object sender, TimerEventArgs e);
        public event RefreshHandler? TimeStopped;
        public event RefreshHandler? Refresh;


        public GameTimer(int periodLength, bool countDown)
        {
            this.countDown = countDown;
            SetTimers(TimeSpan.FromMinutes(periodLength));
            isRunning = false;
        }


        public async Task StartClockAsync()
        {
            cancellationTokenSource = new CancellationTokenSource();
            isRunning = true;

            // Set startTime to get a timestamp
            startTime = DateTime.Now;

            while (isRunning)
            {

                // Wait for the specified interval, to simulate clocktime update
                await Task.Delay(interval, cancellationTokenSource.Token);

                adjustTimers();
                // Trigger refresh event
                OnRefresh();


                // Ensure gameClock don't go below zero
                if (countDown && gameClock.TotalMilliseconds <= interval)
                {
                    gameClock = TimeSpan.Zero;
                    StopClock();
                    break;
                }
                else if (!countDown && gameClock >= periodLength - TimeSpan.FromMilliseconds(interval))
                {
                    gameClock = periodLength;
                    Console.WriteLine(periodLength);
                    StopClock();
                    break;
                }
            }
        }

        private void adjustTimers()
        {
            currentTime = DateTime.Now;             //Set currentTime to get a timestamp
            timePassed = currentTime - startTime;   //Calculates time passed between timestamps
            startTime = currentTime;                //Set startTime to currentTime to get a fresh timestamp for next loop

            // Adjust game clock based on the time passed
            if (countDown)
            {
                gameClock -= timePassed;
            }
            else if (!countDown)
            {
                gameClock += timePassed;
            }
        }

        public void StopClock()
        {
            isRunning = false;
            OnTimeStop();
        }


        public void SetTimers(TimeSpan gameTime)
        {
            if (countDown)
            {
                gameClock = gameTime;
            }
            else if (!countDown)
            {
                gameClock = TimeSpan.Zero;
                periodLength = gameTime;
            }
        }

        public void AdjustTime(TimeSpan adjustment)
        {
            gameClock += adjustment;
            OnTimeStop();
        }

        private void OnTimeStop()
        {
            OnRefresh();
            TimeStopped?.Invoke(this, new TimerEventArgs(this));
        }

        private void OnRefresh()
        {
            Refresh?.Invoke(this, new TimerEventArgs(this));
        }



    }
}
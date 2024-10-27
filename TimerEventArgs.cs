
namespace Scoreboard
{
    public class TimerEventArgs : EventArgs
    {
        public TimeSpan currentTime;
        public bool isRunning;

        public TimerEventArgs(BaseTimer t)
        {
            currentTime = t.CurrentTime;
            isRunning = t.IsRunning;
        }
    }
}
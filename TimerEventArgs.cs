
namespace Scoreboard
{
    public class TimerEventArgs : EventArgs
    {
        public TimeSpan currentTime;

        public TimerEventArgs(BaseTimer t)
        {
            currentTime = t.CurrentTime;
        }
    }
}
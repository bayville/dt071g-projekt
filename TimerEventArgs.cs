using System.Runtime.CompilerServices;

namespace Scoreboard
{
    public class TimerEventArgs : EventArgs
    {
        public TimeSpan gameClock;
        public TimeSpan timePassed;
        public bool isRunning;

        public TimerEventArgs(GameTimer t)
        {
            gameClock = t.gameClock;
            timePassed = t.timePassed;
            isRunning = t.isRunning;
        }
    }
}
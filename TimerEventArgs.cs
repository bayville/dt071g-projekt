namespace Scoreboard
{
        public class TimerEventArgs : EventArgs {
        public TimeSpan gameClock;
        public TimeSpan timePassed;

        public TimerEventArgs(GameTimer t) {
            gameClock = t.gameClock;
            timePassed = t.timePassed;
        }
    }
}
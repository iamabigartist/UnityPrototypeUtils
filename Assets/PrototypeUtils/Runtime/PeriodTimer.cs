using System.Diagnostics;
namespace PrototypeUtils
{

    public class PeriodTimer
    {
        Stopwatch stop_watch;
        double period_ms;

        public PeriodTimer(double period_ms)
        {
            this.period_ms = period_ms;
            stop_watch = new();
        }

        public void StartNewPeriod()
        {
            stop_watch.Restart();
        }
        public bool TimeUp()
        {
            return stop_watch.Get_ms() >= period_ms;
        }
    }
}

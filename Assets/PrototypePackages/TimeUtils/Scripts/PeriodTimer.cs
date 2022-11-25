using System;
using System.Diagnostics;
namespace PrototypePackages.TimeUtil.Scripts
{

    public class PeriodTimer
    {
        Stopwatch stop_watch;
        public double processed_time => TimeProcessor?.Invoke( stop_watch.Get_ms() ) ?? stop_watch.Get_ms();
        public double cur_remain_time_ms
        {
            get
            {
                if (!stop_watch.IsRunning) { return 0; }

                var time = period_ms - processed_time;

                if (time < 0) { time = 0; }

                return time;
            }
        }
        public double period_ms;
        public bool auto_restart;
        public event Func<double, double> TimeProcessor;

        public PeriodTimer(double period_ms, bool auto_restart = false, Func<double, double> timeProcessor = null)
        {
            this.period_ms = period_ms;
            this.auto_restart = auto_restart;
            stop_watch = new();
            TimeProcessor = timeProcessor;
        }

        public void SetTimeUp()
        {
            stop_watch.Stop();
        }
        public void StartNewPeriod()
        {
            stop_watch.Restart();
        }
        public bool IsTimeUp()
        {
            var time_up = !(cur_remain_time_ms > 0);

            if (time_up && auto_restart) { StartNewPeriod(); }

            return time_up;
        }
    }
}

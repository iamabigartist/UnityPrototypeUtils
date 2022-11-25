using System;
using System.Diagnostics;
namespace PrototypePackages.TimeUtils
{
    public class TaskWatch
    {
        public Action task;
        public long max_ticks_per_frame;
        public Stopwatch watch;
        public int total_times;
        public double time_per_msecond => total_times / watch.Get_ms();
        public double msecond_per_time => watch.Get_ms() / total_times;
        public void Run()
        {
            long ticks_until_start = watch.ElapsedTicks;

            while (watch.ElapsedTicks - ticks_until_start < max_ticks_per_frame)
            {
                watch.Start();
                task();
                watch.Stop();
                total_times++;
            }
        }

        public void Reset()
        {
            total_times = 0;
            watch.Reset();
        }

        public TaskWatch(Action task, long max_ticks_per_frame)
        {
            this.task = task;
            this.max_ticks_per_frame = max_ticks_per_frame;
            total_times = 0;
            watch = new();
        }
    }
}

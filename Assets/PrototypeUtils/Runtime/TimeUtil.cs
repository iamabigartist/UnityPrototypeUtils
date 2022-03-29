using System.Diagnostics;
namespace PrototypeUtils
{
    public static class TimeUtil
    {
        public static double Get_ms(this Stopwatch stopwatch)
        {
            return stopwatch.ElapsedTicks / 10000.0;
        }
        public static long MsToTicks(double ms)
        {
            return (long)(ms * 10000.0);
        }
    }
}

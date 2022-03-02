using System.Diagnostics;
namespace PrototypeUtils
{
    public static class DebugUtils
    {
        public static double Get_ms(this Stopwatch stopwatch)
        {
            return stopwatch.ElapsedTicks / 10000.0;
        }
    }
}

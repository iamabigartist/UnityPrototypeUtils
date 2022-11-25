using System.Diagnostics;
namespace PrototypeUtils.TimeUtil
{
	public static class Util
	{
		public static double Get_ms(this Stopwatch stopwatch)
		{
			return stopwatch.ElapsedTicks / 10000.0;
		}
		public static long MsToTicks(double ms)
		{
			return (long)(ms * 10000.0);
		}

		// public static Timer<TTime> CreateIntervalTimer<TTime>(Timer<TTime>.GetCurrentTimeDelegate GetCurrentTime,
		// 	TTime Interval, Timer<TTime>.MatchCallbackDelegate MatchCallbackBeforeUpdateStamp = null)
		// {
		// 	return new(GetCurrentTime(), GetCurrentTime,
		// 		(LastStamp, Stamp) => (dynamic)LastStamp + Interval <= Stamp,
		// 		(Timer, Stamp) =>
		// 		{
		// 			MatchCallbackBeforeUpdateStamp?.Invoke(Timer, Stamp);
		// 			Timer.TimeStamp = Stamp;
		// 		}
		// 		);
		// }
	}
}
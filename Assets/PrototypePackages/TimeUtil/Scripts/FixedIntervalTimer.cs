using System;
namespace PrototypePackages.TimeUtil.Scripts
{
	public class FixedIntervalTimer<TTime> where TTime : IComparable<TTime>
	{
		public delegate bool TimeUpCompareDelegate(TTime LastStamp, TTime CurrentStamp, TTime Interval);
		Timer<TTime> timer;
		public TTime Interval;
		
		public FixedIntervalTimer(Func<TTime> GetCurrentTime, TimeUpCompareDelegate TimeUp, TTime Interval, TTime StartStamp = default, Timer<TTime>.MatchCallbackDelegate Callback = null)
		{
			this.Interval = Interval;
			timer = new(GetCurrentTime, (LastStamp, CurrentStamp) => TimeUp(LastStamp, CurrentStamp, Interval), StartStamp, Callback);
		}
		public bool Elapsed()
		{
			return timer.CheckMatch();
		}
	}
}
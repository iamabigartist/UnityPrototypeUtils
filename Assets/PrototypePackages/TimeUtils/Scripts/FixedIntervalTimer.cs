using System;
namespace PrototypePackages.TimeUtils
{
	public class FixedIntervalTimer<TTime> where TTime : IComparable<TTime>
	{
		public delegate bool TimeUpCompareDelegate(TTime LastStamp, TTime CurrentStamp, TTime Interval);
		public delegate TTime GetNewStampDelegate(TTime LastStamp, TTime CurrentStamp, TTime Interval);
		Timer<TTime> timer;
		public TTime Interval;
		public TTime Stamp => timer.Stamp;

		public FixedIntervalTimer(Func<TTime> GetCurrentTime, TimeUpCompareDelegate TimeUp, GetNewStampDelegate GetNewStamp, TTime Interval, TTime StartStamp = default, Timer<TTime>.MatchCallbackDelegate Callback = null)
		{
			this.Interval = Interval;
			timer = new(GetCurrentTime, (LastStamp, CurrentStamp) => TimeUp(LastStamp, CurrentStamp, Interval), StartStamp, (Stamp, CurrentStamp) =>
			{
				Callback?.Invoke(Stamp, CurrentStamp);
				timer.Stamp = GetNewStamp(Stamp, CurrentStamp, Interval);
			});
		}

		public bool Elapsed()
		{
			return timer.CheckMatch();
		}

		public class Factory : ForFactory<FixedIntervalTimer<TTime>, (Func<TTime> GetCurrentTime, TimeUpCompareDelegate TimeUp, GetNewStampDelegate GetNewStamp), (TTime Interval, TTime StartStamp, Timer<TTime>.MatchCallbackDelegate Callback )>
		{
			public override DoFor CreateKind((Func<TTime> GetCurrentTime, TimeUpCompareDelegate TimeUp, GetNewStampDelegate GetNewStamp) Param)
			{
				(Func<TTime> get_current_time, TimeUpCompareDelegate time_up, GetNewStampDelegate get_new_stamp) = Param;
				return Time =>
				{
					(TTime interval, TTime start_stamp, Timer<TTime>.MatchCallbackDelegate callback) = Time;
					return new(get_current_time, time_up, get_new_stamp, interval, start_stamp, callback);
				};
			}
		}
	}
}
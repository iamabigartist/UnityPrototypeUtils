using System;
namespace PrototypeUtils.TimeUtil
{
	public static class CounterUtil<TCount>
	{
		public delegate void ElapsedMethod(Counter sender_counter);
		public delegate Counter CountFor(TCount Interval, params ElapsedMethod[] callbacks);

		public static CountFor CreateCounterKind(Func<TCount> GetCurrentCount)
		{
			Timer<float> a;
			return (Interval, Callbacks) =>
			{
				var counter = new Counter()
				{
					Interval = Interval,
					GetCurrentCount = GetCurrentCount
				};
				foreach (var callback in Callbacks) { counter.OnStampUpdate += callback; }
				return counter;
			};
		}

		public class Counter
		{
			public TCount Interval;
			public TCount LastCountStamp;
			public Func<TCount> GetCurrentCount;
			public event ElapsedMethod OnStampUpdate;
			public bool Elapsed()
			{
				var cur_count = GetCurrentCount();
				if ((dynamic)cur_count - LastCountStamp >= Interval)
				{
					LastCountStamp = cur_count;
					OnStampUpdate?.Invoke(this);
					return true;
				}
				return false;
			}
		}

	}
}
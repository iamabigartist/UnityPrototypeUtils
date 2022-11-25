using System;
namespace PrototypePackages.TimeUtil.Scripts
{
	//2022/11/21 决定，还是把对象式的计时器写回来，下次记得写完先存下git。具体计时器就继承原本计时器这样了。另外源自counter的初始化方法，最好也加上，相当于给定一部分参数假设，变化另外一部分，缩小了函数范围。
	public class Timer<TTime> where TTime : IComparable<TTime>
	{
		public static bool CompareTimeUpDynamic(TTime LastStamp, TTime CurrentStamp, TTime Interval) =>
			(dynamic)LastStamp + Interval <= CurrentStamp;

		public delegate bool ConditionMatchDelegate(TTime LastStamp, TTime CurrentStamp);
		public delegate void MatchCallbackDelegate(TTime LastStamp, TTime CurrentStamp);

		public TTime Stamp;
		public Func<TTime> GetCurrentTime;
		public ConditionMatchDelegate StampMatch;
		public MatchCallbackDelegate Callback;

		public Timer(Func<TTime> GetCurrentTime, ConditionMatchDelegate StampMatch, TTime StartStamp = default, MatchCallbackDelegate Callback = null)
		{
			Stamp = StartStamp;
			this.GetCurrentTime = GetCurrentTime;
			this.StampMatch = StampMatch;
			this.Callback = Callback;
		}

		public bool CheckMatch()
		{
			var cur_stamp = GetCurrentTime();
			bool match = StampMatch(Stamp, cur_stamp);
			if (match) { Callback?.Invoke(Stamp, cur_stamp); }
			return match;
		}
	}
}
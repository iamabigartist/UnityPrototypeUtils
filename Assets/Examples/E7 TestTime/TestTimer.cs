using System.Threading.Tasks;
using PrototypePackages.TimeUtils;
using UnityEngine;
using static System.Linq.Enumerable;
using static PrototypePackages.TimeUtils.FixedIntervalTimer<float>;
using static PrototypePackages.TimeUtils.ForFactory<PrototypePackages.TimeUtils.FixedIntervalTimer<float>, (System.Func<float> GetCurrentTime, PrototypePackages.TimeUtils.FixedIntervalTimer<float>.TimeUpCompareDelegate TimeUp, PrototypePackages.TimeUtils.FixedIntervalTimer<float>.GetNewStampDelegate GetNewStamp), (float Interval, float StartStamp, PrototypePackages.TimeUtils.Timer<float>.MatchCallbackDelegate Callback)>;
namespace Examples.E7_TestTime
{
	public class TestTimer : MonoBehaviour
	{
		public int count;
		float cur_time;
		DoFor TimeFor;
		FixedIntervalTimer<float>[] timers;
		void Start()
		{
			TimeFor = new Factory().CreateKind((() => cur_time, (Stamp, CurrentStamp, Interval) => Stamp + Interval <= CurrentStamp, (Stamp, _, Interval) => Stamp + Interval));
			timers = Range(0, count).Select(i => TimeFor((1f, i, null))).ToArray();
		}
		void FixedUpdate()
		{
			cur_time = Time.fixedTime;
			
			Parallel.ForEach(timers, timer => timer.Elapsed());
			Debug.Log($"Current: {timers[0].Stamp}");
		}
	}
}
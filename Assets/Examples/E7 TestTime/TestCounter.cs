using UnityEngine;
using static PrototypePackages.TimeUtils.CounterUtil<float>;
namespace Examples.E7_TestTime
{
	public class TestCounter : MonoBehaviour
	{
		CountFor CountFor = CreateCounterKind(() => Time.time);
		Counter log_0;
		void Start()
		{
			log_0 = CountFor(1, _ => Debug.Log("Log 0"));
		}

		void Update()
		{
			log_0.Elapsed();
		}
	}
}
﻿using System;
namespace PrototypePackages.TimeUtils
{
	public class CheckIntervalTimer<TTime> 
	{
		Timer<TTime> timer;
		TTime cur_interval;
		
		public CheckIntervalTimer(Func<TTime> GetCurrentTime, TTime StartStamp = default)
		{
			timer = new(GetCurrentTime, (Stamp, CurrentStamp) => Timer<TTime>.CompareTimeUpDynamic(Stamp, CurrentStamp, cur_interval), StartStamp);
		}

		public bool HasLastFor(TTime interval)
		{
			cur_interval = interval;
			return timer.CheckMatch();
		}

		public void Reset()
		{
			timer.Stamp = timer.GetCurrentTime();
		}
	}
}
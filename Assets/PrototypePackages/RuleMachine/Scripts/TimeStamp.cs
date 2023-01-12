namespace PrototypePackages.RuleMachine
{
	public struct TimeStamp
	{
		public double Value;
		public TimeStamp(double InitialValue = 0)
		{
			Value = InitialValue;
		}
		public double RemainTime(double CurTime, double Interval) { return Value + Interval - CurTime; }
		public bool TimeUp(double CurTime, double Interval) { return Value + Interval <= CurTime; }
	}
}
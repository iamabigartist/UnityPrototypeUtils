namespace Examples.E9_TestBonePlating
{
	public struct Resource
	{
		public float Max;
		public float Cur;
		public Resource(float Max, float Cur)
		{
			this.Max = Max;
			this.Cur = Cur;
		}
		public void Fill() { Cur = Max; }
	}
}
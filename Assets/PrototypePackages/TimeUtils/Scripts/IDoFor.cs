namespace PrototypePackages.TimeUtils
{
	public abstract class ForFactory<TDo, TKindParam, TInstanceParam>
	{
		public delegate TDo DoFor(TInstanceParam Param);
		public abstract DoFor CreateKind(TKindParam Param);
	}
}
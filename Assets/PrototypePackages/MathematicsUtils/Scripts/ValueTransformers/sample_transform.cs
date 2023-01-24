namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public struct sample_transform<T>
		where T : struct
	{
		public T offset;
		public T scale;
		public sample_transform(T offset, T scale)
		{
			this.offset = offset;
			this.scale = scale;
		}
	}
}
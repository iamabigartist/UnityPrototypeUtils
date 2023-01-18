namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public struct FloatTransformer:IValueTransformer<float>
	{
		public float scale;
		public float offset;
		public FloatTransformer(float scale, float offset)
		{
			this.scale = scale;
			this.offset = offset;
		}
		public void Transform(in float original_value, out float transformed_value)
		{
			transformed_value = original_value * scale + offset;
		}
	}
}
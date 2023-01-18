using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public struct Float2Transformer : IValueTransformer<float2>
	{
		public float2 scale;
		public float2 offset;
		public Float2Transformer(float2 scale, float2 offset)
		{
			this.scale = scale;
			this.offset = offset;
		}
		public void Transform(in float2 original_value, out float2 transformed_value)
		{
			transformed_value = original_value * scale + offset;
		}
	}
}
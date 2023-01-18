using System.Runtime.CompilerServices;
using PrototypePackages.MathematicsUtils.ValueTransformers;
using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.Noise
{
	public struct PlaneHeightNoise<TCoherentNoise> : ICoherentNoise<float2, float>
		where TCoherentNoise : ICoherentNoise<float2, float>
	{
		Float2Transformer position_transformer;
		FloatTransformer height_transformer;
		TCoherentNoise source_noise;
		public PlaneHeightNoise(
			float2 position_scale, float2 position_offset,
			float height_scale, float height_offset,
			TCoherentNoise source_noise
		)
		{
			position_transformer = new(position_scale, position_offset);
			height_transformer = new(height_scale, height_offset);
			this.source_noise = source_noise;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Sample(in float2 input_pos, out float output_height)
		{
			position_transformer.Transform(input_pos, out var sample_pos);
			source_noise.Sample(sample_pos, out var noise_value);
			height_transformer.Transform(noise_value, out output_height);
		}
	}
}
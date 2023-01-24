using System.Runtime.CompilerServices;
using PrototypePackages.MathematicsUtils.ValueTransformers;
using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.Noise
{
	public struct PlaneHeightNoise<TCoherentNoise> : ICoherentNoise<float2, float>
		where TCoherentNoise : ICoherentNoise<float2, float>
	{
		sampler_f2 position_sampler;
		sampler_f height_sampler;
		TCoherentNoise source_noise;
		public PlaneHeightNoise(
			float2 position_scale, float2 position_offset,
			float height_scale, float height_offset,
			TCoherentNoise source_noise
		)
		{
			position_sampler = new(new(position_offset, position_scale));
			height_sampler = new(new(height_offset, height_scale));
			this.source_noise = source_noise;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Sample(in float2 input_pos, out float output_height)
		{ 
			position_sampler.TransformIn(input_pos, out var sample_pos);
			source_noise.Sample(sample_pos, out var noise_value);
			height_sampler.TransformOut(noise_value, out output_height);
		}
	}
}
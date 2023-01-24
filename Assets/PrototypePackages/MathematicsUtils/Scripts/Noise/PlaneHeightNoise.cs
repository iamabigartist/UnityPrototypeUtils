using System.Runtime.CompilerServices;
using PrototypePackages.MathematicsUtils.ValueTransformers;
using Unity.Mathematics;
using static PrototypePackages.MathematicsUtils.ValueTransformers.Sampler;
namespace PrototypePackages.MathematicsUtils.Noise
{
	public struct PlaneHeightNoise<TCoherentNoise> : ICoherentNoise<float2, float>
		where TCoherentNoise : ICoherentNoise<float2, float>
	{
		sample_transform<float2, op_f2> position_transform;
		sample_transform<float, op_f> height_transform;
		TCoherentNoise source_noise;
		public PlaneHeightNoise(sample_transform<float2, op_f2> position_transform, sample_transform<float, op_f> height_transform, TCoherentNoise source_noise)
		{
			this.position_transform = position_transform;
			this.height_transform = height_transform;
			this.source_noise = source_noise;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Sample(in float2 input_pos, out float output_height)
		{
			position_transform.TransformIn(input_pos, out var sample_pos);
			source_noise.Sample(sample_pos, out var noise_value);
			height_transform.TransformOut(noise_value, out output_height);
		}
	}
}
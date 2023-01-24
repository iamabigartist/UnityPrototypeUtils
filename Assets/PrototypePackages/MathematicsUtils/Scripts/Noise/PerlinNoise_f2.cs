using System.Runtime.CompilerServices;
using PrototypePackages.MathematicsUtils.ValueTransformers;
using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.Noise
{
	public struct PerlinNoise_f2 : ICoherentNoise<float2, float>
	{
		public sample_transform<float> transform;
		public float2 rep;
		public PerlinNoise_f2(sample_transform<float> transform, float2 rep)
		{
			this.transform = transform;
			this.rep = rep;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Sample(in float2 input, out float output)
		{
			output = (noise.pnoise(input, rep) + transform.offset) * transform.scale;
		}
	}
}
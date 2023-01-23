using System.Runtime.CompilerServices;
using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.Noise
{
	public struct PerlinNoise_f2 : ICoherentNoise<float2, float>
	{
		public float value_scale;
		public float value_offset;
		public float2 rep;
		public PerlinNoise_f2(float value_scale, float value_offset, float2 rep)
		{
			this.rep = rep;
			this.value_scale = value_scale;
			this.value_offset = value_offset;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Sample(in float2 input, out float output)
		{
			output = noise.pnoise(input, rep) * value_scale + value_offset;
		}
	}
}
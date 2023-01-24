using System.Runtime.CompilerServices;
using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.Noise
{
	public struct PerlinNoise_f2 : ICoherentNoise<float2, float>
	{
		public float2 rep;
		public PerlinNoise_f2(float2 rep)
		{
			this.rep = rep;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Sample(in float2 input, out float output)
		{
			output = (noise.pnoise(input, rep) + 1) / 2f;
		}
	}
}
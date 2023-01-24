using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static PrototypePackages.MathematicsUtils.ValueTransformers.Sampler;
namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public struct sampler_f2 : ISampler<float2>
	{
		public sampler_f2(sample_transform<float2> transform)
		{
			this.transform = transform;
		}
		public sample_transform<float2> transform { get; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 add(in float2 a, in float2 b) => a + b;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 mul(in float2 a, in float2 b) => a * b;
	}
}
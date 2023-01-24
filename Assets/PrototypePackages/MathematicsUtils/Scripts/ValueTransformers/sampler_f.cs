using System;
using System.Runtime.CompilerServices;
using static PrototypePackages.MathematicsUtils.ValueTransformers.Sampler;
namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public struct sampler_f : ISampler<float>
	{
		public sampler_f(sample_transform<float> transform)
		{
			this.transform = transform;
		}
		public sample_transform<float> transform { get; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float add(in float a, in float b) => a + b;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float mul(in float a, in float b) => a * b;
	}
}
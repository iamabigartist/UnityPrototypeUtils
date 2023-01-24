using System.Runtime.CompilerServices;
using static PrototypePackages.MathematicsUtils.ValueTransformers.Sampler;
namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public struct op_f : INumericOperators<float>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float add(in float a, in float b) => a + b;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float mul(in float a, in float b) => a * b;
	}
}
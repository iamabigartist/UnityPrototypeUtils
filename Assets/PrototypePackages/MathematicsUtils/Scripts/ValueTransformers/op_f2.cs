using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static PrototypePackages.MathematicsUtils.ValueTransformers.Sampler;
namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public struct op_f2 : INumericOperators<float2>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 add(in float2 a, in float2 b) => a + b;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 mul(in float2 a, in float2 b) => a * b;
	}
}
using System.Runtime.CompilerServices;
namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public static class Sampler
	{
		public interface ISampler<TValue> where TValue : struct
		{
			sample_transform<TValue> transform { get; }
			TValue add(in TValue a, in TValue b);
			TValue mul(in TValue a, in TValue b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TransformIn<TSampler, TValue>(this TSampler sampler, in TValue value, out TValue value_in)
			where TSampler : ISampler<TValue>
			where TValue : struct
		{
			value_in = sampler.mul(sampler.add(value, sampler.transform.offset), sampler.transform.scale);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TransformOut<TSampler, TValue>(this TSampler sampler, in TValue value, out TValue value_out)
			where TSampler : ISampler<TValue>
			where TValue : struct
		{
			value_out = sampler.add(sampler.mul(value, sampler.transform.scale), sampler.transform.offset);
		}
	}


}
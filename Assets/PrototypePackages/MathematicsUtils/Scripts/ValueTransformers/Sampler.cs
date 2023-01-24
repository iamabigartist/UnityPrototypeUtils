using System.Runtime.CompilerServices;
namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public static class Sampler
	{
		public interface INumericOperators<TValue> where TValue : struct
		{
			TValue add(in TValue a, in TValue b);
			TValue mul(in TValue a, in TValue b);
		}

		public struct sample_transform<TValue, TOperator>
			where TValue : struct
			where TOperator : struct, INumericOperators<TValue>
		{
			public TOperator op;
			public TValue scale;
			public TValue offset;
			public sample_transform(TValue scale, TValue offset)
			{
				op = new();
				this.scale = scale;
				this.offset = offset;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TransformIn<TOperator, TValue>(this sample_transform<TValue, TOperator> transform, in TValue value, out TValue value_in)
			where TValue : struct
			where TOperator : struct, INumericOperators<TValue>
		{
			value_in = transform.op.mul(transform.op.add(value, transform.offset), transform.scale);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void TransformOut<TOperator, TValue>(this sample_transform<TValue, TOperator> transform, in TValue value, out TValue value_out)
			where TValue : struct
			where TOperator : struct, INumericOperators<TValue>
		{
			value_out = transform.op.add(transform.op.mul(value, transform.scale), transform.offset);
		}
	}


}
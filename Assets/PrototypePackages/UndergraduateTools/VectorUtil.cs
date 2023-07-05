using Unity.Collections;
using static Unity.Collections.LowLevel.Unsafe.UnsafeUtility;
namespace PrototypePackages.UndergraduateTools
{
public static unsafe class VectorUtil
{
	public static void CombineArray<T1, T2, T3>(NativeArray<T1> a1, NativeArray<T2> a2, NativeArray<T3> a3)
		where T1 : struct
		where T2 : struct
		where T3 : struct
	{
		var size_1 = SizeOf<T1>();
		var size_2 = SizeOf<T2>();
		var size_3 = SizeOf<T3>();
		a3.Slice().SliceWithStride<T1>().CopyFrom(a1);
		a3.Slice().SliceWithStride<T2>(size_1).CopyFrom(a2);
	}
}
}
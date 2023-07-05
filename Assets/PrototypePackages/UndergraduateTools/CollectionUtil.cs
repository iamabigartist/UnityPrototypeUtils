using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using static Unity.Collections.LowLevel.Unsafe.UnsafeUtility;
namespace PrototypePackages.UndergraduateTools
{
public static unsafe class CollectionUtil
{

#region Enumerate

	public static string ListLog<T>(this IEnumerable<T> list, string separator = ", ")
	{
		return string.Join(separator, list);
	}

	public static IEnumerable<T> Enumerate<T>(this UnsafeList<T> list)
		where T : unmanaged
	{
		for (int i = 0; i < list.Length; i++)
		{
			yield return list[i];
		}
	}

#endregion

#region Convert

	public static NativeArray<T> ToNativeArray<T>(this UnsafeList<T> unsafe_list, Allocator allocator)
		where T : unmanaged
	{
		return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(unsafe_list.Ptr, unsafe_list.Length, allocator);
	}

	public static NativeList<T> ToNativeList<T>(this NativeArray<T> array, Allocator allocator, bool dispose_old=true)
		where T : unmanaged
	{
		var list = new NativeList<T>(array.Length, allocator);
		list.CopyFrom(array);
		if (dispose_old) { array.Dispose(); }
		return list;
	}

#endregion

#region Dispose

	public static void DisposeElements<T>(this UnsafeList<T> list) where T : unmanaged, IDisposable
	{
		for (int i = 0; i < list.Length; i++) { list[i].Dispose(); }
	}

	public static void DisposeAll(params IDisposable[] disposables)
	{
		foreach (var disposable in disposables) { disposable?.Dispose(); }
	}

#endregion

#region Memory

	public static ref T Get<T>(this UnsafeList<T> list, int index) where T : unmanaged
	{
		return ref ArrayElementAsRef<T>(list.Ptr, index);
	}

	public static T* AllocCreate<T>(T value, Allocator allocator) where T : unmanaged
	{
		var ptr = (T*)Malloc(SizeOf<T>(), AlignOf<T>(), allocator);
		*ptr = value;
		return ptr;
	}

	public static void AllocDestroy<T>(T* ptr, Allocator allocator) where T : unmanaged
	{
		Free(ptr, allocator);
	}

#endregion

}
}
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;
namespace PrototypePackages.MathematicsUtils.Index
{
	public readonly struct Index2D
	{
		public readonly int2 size;

		public Index2D(int2 size)
		{
			this.size = size;
		}

	#region Property

		public int Count => size.x * size.y;
		public int2 CenterPoint => size / 2;

	#endregion

	#region Indexer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void To1D(in int x, in int y, out int i)
		{
			i = x + y * size.x;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void To2D(int i, out int x, out int y)
		{
			y = i / size.x;
			i -= y * size.x;
			x = i;
		}

	#endregion

	#region Util

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(in int x, in int y)
		{
			return
				x < 0 || x > size.x - 1 ||
				y < 0 || y > size.y - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(in int2 p) => OutOfRange(p.x, p.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(in int x, in int y)
		{
			return
				x == 0 || x == size.x - 1 ||
				y == 0 || y == size.y - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(in int2 p) => IsEdge(p.x, p.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(in int x, in int y)
		{
			return
				x == size.x - 1 ||
				y == size.y - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(in int2 p) => IsPositiveEdge(p.x, p.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2 SampleUV(in float u, in float v)
		{
			return (int2)round(new float2(size.x * u - 0.5f, size.y * v - 0.5f));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2 RepeatWrap(in int2 p)
		{
			var result = p;
			if (p.x >= size.x) { result.x -= size.x; }
			if (p.y >= size.y) { result.y -= size.y; }
			return result;
		}

	#endregion
	}
}
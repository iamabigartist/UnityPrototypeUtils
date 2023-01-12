using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;
namespace PrototypePackages.JobUtils
{

	public static class IndexUtil {}

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

		public int this[int x, int y]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => x + y * size.x;
		}

		public int this[int2 p]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => this[p.x, p.y];
		}

		public int2 this[int i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				int y = i / size.x;
				i -= y * size.x;
				int x = i;

				return new(x, y);
			}
		}

	#endregion

	#region Util

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(int x, int y)
		{
			return
				x < 0 || x > size.x - 1 ||
				y < 0 || y > size.y - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(int2 p) => OutOfRange(p.x, p.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(int x, int y)
		{
			return
				x == 0 || x == size.x - 1 ||
				y == 0 || y == size.y - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(int2 p) => IsEdge(p.x, p.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(int x, int y)
		{
			return
				x == size.x - 1 ||
				y == size.y - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(int2 p) => IsPositiveEdge(p.x, p.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2 SampleUV(float u, float v)
		{
			return (int2)round(new float2(size.x * u - 0.5f, size.y * v - 0.5f));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2 RepeatWrap(int2 p)
		{
			var result = p;
			if (p.x >= size.x) { result.x -= size.x; }
			if (p.y >= size.y) { result.y -= size.y; }
			return result;
		}

	#endregion
	}

	public readonly struct Index3D
	{
		public readonly int3 size;

		public Index3D(int3 size)
		{
			this.size = size;
		}

	#region Property

		public int Count => size.x * size.y * size.z;
		public int3 CenterPoint => size / 2;

	#endregion

	#region Indexer

		public int this[int x, int y, int z]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => x + y * size.x + z * size.y * size.x;
		}
		public int this[int3 p]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => this[p.x, p.y, p.z];
		}

		public int3 this[int i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				int z = i / (size.x * size.y);
				i -= z * size.x * size.y;
				int y = i / size.x;
				i -= y * size.x;
				int x = i;

				return new(x, y, z);
			}
		}

	#endregion

	#region Util

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(int x, int y, int z)
		{
			return
				x < 0 || x > size.x - 1 ||
				y < 0 || y > size.y - 1 ||
				z < 0 || z > size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(int3 p) => OutOfRange(p.x, p.y, p.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(int x, int y, int z)
		{
			return
				x == 0 || x == size.x - 1 ||
				y == 0 || y == size.y - 1 ||
				z == 0 || z == size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(int3 p) => IsEdge(p.x, p.y, p.z);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(int x, int y, int z)
		{
			return
				x == size.x - 1 ||
				y == size.y - 1 ||
				z == size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(int3 p) => IsPositiveEdge(p.x, p.y, p.z);

	#endregion
	}
}
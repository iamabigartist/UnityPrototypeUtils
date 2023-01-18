using System.Runtime.CompilerServices;
using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.Index
{
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
			
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void To1D(in int x, in int y, in int z, out int i)
		{
			i = x + y * size.x + z * size.y * size.x;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void To3D(int i, out int x, out int y, out int z)
		{
			z = i / (size.x * size.y);
			i -= z * size.x * size.y;
			y = i / size.x;
			i -= y * size.x;
			x = i;
		}

	#endregion

	#region Util

			

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(in int x,in int y,in int z)
		{
			return
				x < 0 || x > size.x - 1 ||
				y < 0 || y > size.y - 1 ||
				z < 0 || z > size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(in int3 p) => OutOfRange(p.x, p.y, p.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(in int x,in int y,in int z)
		{
			return
				x == 0 || x == size.x - 1 ||
				y == 0 || y == size.y - 1 ||
				z == 0 || z == size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(in int3 p) => IsEdge(p.x, p.y, p.z);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(in int x,in int y,in int z)
		{
			return
				x == size.x - 1 ||
				y == size.y - 1 ||
				z == size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(in int3 p) => IsPositiveEdge(p.x, p.y, p.z);

	#endregion
	}

}
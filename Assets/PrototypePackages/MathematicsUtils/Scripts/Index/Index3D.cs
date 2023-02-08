using System.Runtime.CompilerServices;
using Unity.Mathematics;
namespace PrototypePackages.MathematicsUtils.Index
{
	public readonly struct Index3D
	{
		public readonly int3 size;
		public readonly int size_xy;

		public Index3D(int3 size)
		{
			this.size = size;
			size_xy = size.x * size.y;
		}

	#region Property

		public int Count => size.x * size.y * size.z;
		public int3 CenterPoint => size / 2;

	#endregion

	#region Indexer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void To1D(in int x, in int y, in int z, out int i)
		{
			i = x + y * size.x + z * size_xy;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void To3D(int i, out int x, out int y, out int z)
		{
			z = i / size_xy;
			i -= z * size_xy;
			y = i / size.x;
			i -= y * size.x;
			x = i;
		}

	#endregion

	#region Mover

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveX(in int i, in int x_step, out int moved_i)
		{
			moved_i = i + x_step;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveY(in int i, in int y_step, out int moved_i)
		{
			moved_i = i + y_step * size.x;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MoveZ(in int i, in int z_step, out int moved_i)
		{
			moved_i = i + z_step * size_xy;
		}

	#endregion

	#region Util

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(in int x, in int y, in int z)
		{
			return
				x < 0 || x > size.x - 1 ||
				y < 0 || y > size.y - 1 ||
				z < 0 || z > size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool OutOfRange(in int3 p) => OutOfRange(p.x, p.y, p.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(in int x, in int y, in int z)
		{
			return
				x == 0 || x == size.x - 1 ||
				y == 0 || y == size.y - 1 ||
				z == 0 || z == size.z - 1;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEdge(in int3 p) => IsEdge(p.x, p.y, p.z);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPositiveEdge(in int x, in int y, in int z)
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
﻿using Unity.Mathematics;
namespace PrototypePackages.JobUtils
{
	/// <summary>
	///     是否有必要使用来简化代码？
	/// </summary>
	public struct IndexRandGenerator
	{
		public uint seed;
		public IndexRandGenerator(uint seed = 0)
		{
			this.seed = seed;
		}
		uint RandIndex_XOR(uint i)
		{
			return seed ^ i;
		}
		public void Gen(int i, out Random rand) { rand = Random.CreateFromIndex(RandIndex_XOR((uint)i)); }
	}
}
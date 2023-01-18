using Unity.Mathematics;
using UnityEngine;
namespace PrototypePackages.MathematicsUtils.Vector
{
	public static class VectorUtils
	{
		public static int area(this int2 i2) { return i2.x * i2.y; }
		public static int volume(this int3 i3) { return i3.x * i3.y * i3.z; }
		public static float3 f3(this Color color) { return new(color.r, color.g, color.b); }
		public static float2 f2(this Vector2 v) { return new(v.x, v.y); }
		public static int2 i2(this Vector2 v) { return (int2)new float2(v.x, v.y); }
	}
}
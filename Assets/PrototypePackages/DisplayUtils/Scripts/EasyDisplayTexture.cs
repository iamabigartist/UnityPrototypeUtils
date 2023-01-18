using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
namespace PrototypePackages.DisplayUtils
{
	public static class EasyDisplayTexture
	{
		public static int2 Size(this Texture2D t) => new(t.width, t.height);
		public static void CreateDisplayTexture(int2 size, out Texture2D t)
		{
			t = new(size.x, size.y, TextureFormat.RGBAFloat, false) { filterMode = FilterMode.Point };
		}

	#region Interface

		public static void GetTextureSlice<TSliceStride>(this Texture2D t, out NativeSlice<TSliceStride> Slice, int float_offset_count) where TSliceStride : struct
		{
			Slice = t.GetRawTextureData<float4>().Slice().
				SliceWithStride<TSliceStride>(sizeof(float) * float_offset_count);
		}

		public static void SetTextureSlice<TSliceStride>(this Texture2D t, NativeArray<TSliceStride> Result, int float_offset_count) where TSliceStride : struct
		{
			var slice = t.GetRawTextureData<float4>().Slice().
				SliceWithStride<TSliceStride>(sizeof(float) * float_offset_count);
			slice.CopyFrom(Result);
		}

		public static void SetAlpha(this Texture2D t, float Alpha)
		{
			t.GetTextureSlice<float>(out var alpha_slice, 3);
			alpha_slice.CopyFrom(Enumerable.Repeat(Alpha, alpha_slice.Length).ToArray());
		}

		public static void ApplyRGBResult(this Texture2D t, NativeArray<float3> ResultRGB, float Alpha)
		{
			t.GetTextureSlice<float3>(out var rgb_slice, 0);
			rgb_slice.CopyFrom(ResultRGB);
			t.SetAlpha(Alpha);
			t.Apply();
		}

	#endregion
	}
}
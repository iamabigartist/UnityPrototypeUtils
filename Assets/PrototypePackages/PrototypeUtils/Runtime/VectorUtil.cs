using System;
using Unity.Mathematics;
using UnityEngine;
namespace PrototypePackages.PrototypeUtils.Runtime
{
    public static class VectorUtil
    {

        [Serializable]
        public struct Triangle
        {
            public Vector3 x;
            public Vector3 y;
            public Vector3 z;
            public Triangle(Vector3 x, Vector3 y, Vector3 z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

    #region VectorEaseDefine

        public static Vector2 V(this (float x, float y) tuple)
        {
            return new(tuple.x, tuple.y);
        }
        public static float2 f2(this (float x, float y) tuple)
        {
            return new(tuple.x, tuple.y);
        }
        public static Vector3 V(this (float x, float y, float z) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z);
        }
        public static float3 f3(this (float x, float y, float z) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z);
        }
        public static float3 f3(this (int x, int y, int z) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z);
        }
        public static Vector4 V(this (float x, float y, float z, float w) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z, tuple.w);
        }
        public static float4 f4(this (float x, float y, float z, float w) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z, tuple.w);
        }

        public static Vector2Int V(this (int x, int y) tuple)
        {
            return new(tuple.x, tuple.y);
        }
        public static int2 i2(this (int x, int y) tuple)
        {
            return new(tuple.x, tuple.y);
        }
        public static Vector3Int V(this (int x, int y, int z) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z);
        }
        public static int3 i3(this (int x, int y, int z) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z);
        }

        public static Vector4 V(this (int x, int y, int z, int w) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z, tuple.w);
        }
        public static float4 f4(this (int x, int y, int z, int w) tuple)
        {
            return new(tuple.x, tuple.y, tuple.z, tuple.w);
        }

        public static void Deconstruct(this Vector3Int v, out int x, out int y, out int z)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
        public static void Deconstruct(this Vector3 v, out float x, out float y, out float z)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

    #endregion

    #region Range

        public static Range Rg(this Vector2Int v)
        {
            return v.x..v.y;
        }

    #endregion

    #region Precision

        public static Vector3 Round(this Vector3 v, int digit, MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return (
                (float)Math.Round( v.x, digit, rounding ),
                (float)Math.Round( v.y, digit, rounding ),
                (float)Math.Round( v.z, digit, rounding )).V();
        }

        public static float3 Round(this float3 f3, int digit, MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return new(
                (float)Math.Round( f3.x, digit, rounding ),
                (float)Math.Round( f3.y, digit, rounding ),
                (float)Math.Round( f3.z, digit, rounding ));
        }

        public static float4 Round(this float4 f3, int digit, MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return new(
                (float)Math.Round( f3.x, digit, rounding ),
                (float)Math.Round( f3.y, digit, rounding ),
                (float)Math.Round( f3.z, digit, rounding ),
                (float)Math.Round( f3.w, digit, rounding ));
        }

        public static Vector3 IntRound(this Vector3 v)
        {
            return new(
                Mathf.Round( v.x ),
                Mathf.Round( v.y ),
                Mathf.Round( v.z ));
        }

        public static Vector3Int IntRound2Int(this Vector3 v)
        {
            return new(
                Mathf.RoundToInt( v.x ),
                Mathf.RoundToInt( v.y ),
                Mathf.RoundToInt( v.z ));
        }

    #endregion

    #region Convert

        public static Vector4 ToVector4(this Vector3Int v, float w = default)
        {
            return new(v.x, v.y, v.z, w);
        }
        public static Vector4 ToVector4(this Vector3 v, float w = default)
        {
            return new(v.x, v.y, v.z, w);
        }

        public static Vector3 ToVector(this float3 f)
        {
            return new(f.x, f.y, f.z);
        }
        public static Vector2 ToVector(this int2 i)
        {
            return new(i.x, i.y);
        }
        public static Vector2 ToVector(this float2 i)
        {
            return new(i.x, i.y);
        }
        public static Quaternion ToQuaternion(this Vector3 v)
        {
            return Quaternion.Euler( v );
        }

        public static Color ToColor(this Vector3 v)
        {
            return new(v.x, v.y, v.z, 0);
        }
        public static Vector3[] ToVectorArray(this float3[] array)
        {
            var v_array = new Vector3[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                v_array[i] = array[i].ToVector();
            }

            return v_array;
        }
        public static Vector2[] ToVectorArray(this float2[] array)
        {
            var v_array = new Vector2[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                v_array[i] = array[i].ToVector();
            }

            return v_array;
        }
        public static Vector2[] ToVectorArray(this int2[] array)
        {
            var v_array = new Vector2[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                v_array[i] = array[i].ToVector();
            }

            return v_array;
        }

    #endregion

    #region Calculated

        public static int XYZProduct(this Vector3Int v)
        {
            return v.x * v.y * v.z;
        }

    #endregion
    }
}

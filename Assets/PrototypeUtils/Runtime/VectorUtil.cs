using System;
using UnityEngine;
namespace PrototypeUtils
{
    public static class VectorUtil
    {

    #region VectorEaseDefine

        public static Vector2 V(this (float x, float y) tuple)
        {
            return new Vector2( tuple.x, tuple.y );
        }
        public static Vector3 V(this (float x, float y, float z) tuple)
        {
            return new Vector3( tuple.x, tuple.y, tuple.z );
        }
        public static Vector4 V(this (float x, float y, float z, float w) tuple)
        {
            return new Vector4( tuple.x, tuple.y, tuple.z, tuple.w );
        }

        public static Vector2Int V(this (int x, int y) tuple)
        {
            return new Vector2Int( tuple.x, tuple.y );
        }
        public static Vector3Int V(this (int x, int y, int z) tuple)
        {
            return new Vector3Int( tuple.x, tuple.y, tuple.z );
        }
        public static Vector4 V(this (int x, int y, int z, int w) tuple)
        {
            return new Vector4( tuple.x, tuple.y, tuple.z, tuple.w );
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

    #endregion
    }
}

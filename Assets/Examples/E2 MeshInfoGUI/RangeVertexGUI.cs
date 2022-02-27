using System;
using PrototypeUtils;
using UnityEngine;
using static PrototypeUtils.MeshUtil;
namespace Examples.E2_MeshInfoGUI
{
    public class RangeVertexGUI : MonoBehaviour
    {
        public Camera m_camera;
        Mesh m_mesh;
        int[] tri;
        Vector3[] v;
        Vector2[] uv;

        void GetMesh()
        {
            m_mesh = GetComponent<MeshFilter>().sharedMesh;
            tri = m_mesh.triangles;
            uv = GetUnzippedArray( m_mesh.uv, tri );
            v = GetUnzippedArray( m_mesh.vertices, tri );
            str = GetVertexStringArray();
        }

        string GetVertexString(int i)
        {
            return $"v: {v[i]}\n" +
                   $"i:{i}, tri: {tri[i]}\n" +
                   $"uv: {uv[i]}\n";
        }


        string[] str;
        string[] GetVertexStringArray()
        {
            var info_array = new string[v.Length];

            for (int i = 0; i < v.Length; i++)
            {
                info_array[i] = GetVertexString( i );
            }

            return info_array;
        }

        void DrawRangeMesh(Range r)
        {
            UIUtils.DrawPositionLabelArray( m_camera, v[r], str[r], text_size );
        }

        void Start()
        {
            GetMesh();
        }

        public Vector2Int draw_range;
        public Vector2 text_size;

        void OnGUI()
        {
            DrawRangeMesh( draw_range.Rg() );
        }
    }
}

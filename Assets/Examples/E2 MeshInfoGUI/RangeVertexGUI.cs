using UnityEngine;
namespace Examples.E2_MeshInfoGUI
{
    public class RangeVertexGUI : MonoBehaviour
    {
        Mesh m_mesh;
        Vector3[] v;
        int[] t;
        Vector3[] n;
        Vector2[] uv;

        void GetMesh()
        {
            m_mesh = GetComponent<MeshFilter>().sharedMesh;
            v = m_mesh.vertices;
            t = m_mesh.triangles;
            n = m_mesh.normals;
            uv = m_mesh.uv;
        }

        string GetVertexString(int i)
        {
            return $"v: {v[i]}\n" +
                   $"t: {t[i]}\n" +
                   $"uv: {uv[i]}\n";
        }

        void DrawRangeMesh(int start, int range)
        {
            
        }

        void Start() { }

        void Update() { }
    }
}

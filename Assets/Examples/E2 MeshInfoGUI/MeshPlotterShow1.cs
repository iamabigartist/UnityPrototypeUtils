using PrototypePackages.PrototypeUtils.Runtime;
using UnityEngine;
namespace Examples.E2_MeshInfoGUI
{
    public class MeshPlotterShow1 : MonoBehaviour
    {
        public MeshInfoGUIPlot<( Vector3 v, Vector2 uv)> plot;
        Mesh mesh;
        Vector3 world_pos;
        void Start()
        {
            mesh = GetComponent<MeshFilter>().sharedMesh;
            plot.SetFactory(
                () =>
                {
                    world_pos = transform.position;
                    var tri = mesh.triangles;
                    var v = mesh.vertices;
                    var uv = mesh.uv;
                    var sources = new (Vector3 v, Vector2 uv)[v.Length];

                    for (int i = 0; i < v.Length; i++)
                    {
                        sources[i] = (v[i], uv[i]);
                    }

                    return (sources, tri);
                },
                arg =>
                {
                    var (i, tri, (v, uv)) = arg;
                    var world_v = v + world_pos;
                    var info =
                        $"v: {v}\n" +
                        $"i:{i}, tri: {tri}\n" +
                        $"uv: {uv}\n";

                    return (world_v, info);
                } );
            plot.GenInfo();
        }

        void OnGUI()
        {
            plot.OnGUI();
        }
    }
}

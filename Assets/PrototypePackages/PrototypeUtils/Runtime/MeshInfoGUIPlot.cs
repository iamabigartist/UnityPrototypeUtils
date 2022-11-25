using System;
using UnityEngine;
using static PrototypePackages.PrototypeUtils.Runtime.UIUtils;
namespace PrototypePackages.PrototypeUtils.Runtime
{
    [Serializable]
    public class MeshInfoGUIPlot<TSource>
    {
        public Camera camera;
        public Vector2Int plot_range;
        public Vector2 size;
        public event Func<(TSource[] source, int[] indices )> InfoSource;
        public event Func<(int global_i, int index, TSource source), (Vector3 world_vs, string infos)> InfoFactory;
        public void SetFactory(
            Func<(TSource[] source, int[] indices )> info_source,
            Func<(int global_i, int index, TSource source), (Vector3 world_v, string info)> info_factory)
        {
            InfoSource = info_source;
            InfoFactory = info_factory;
        }

        Vector3[] world_vs;
        string[] infos;

        public void GenInfo()
        {
            (TSource[] source, int[] indices) = InfoSource!.Invoke();
            world_vs = new Vector3[indices.Length];
            infos = new string[indices.Length];

            for (int j = 0; j < indices.Length; j++)
            {
                int i = indices[j];
                (world_vs[i], infos[i]) = InfoFactory!.Invoke( (j, i, source[i]) );
            }
        }

        public void OnGUI()
        {
            DrawPositionLabelArray( camera, world_vs[plot_range.Rg()], infos[plot_range.Rg()], size );
        }
    }
}

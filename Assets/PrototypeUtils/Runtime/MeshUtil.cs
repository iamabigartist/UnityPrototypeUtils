using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
namespace PrototypeUtils
{
    public static class MeshUtil
    {
        public static T[] GetUnzippedArray<T>(T[] zipped_array, int[] indices)
        {
            var unzipped_array = new T[indices.Length];

            for (int i = 0; i < indices.Length; i++)
            {
                unzipped_array[i] = zipped_array[indices[i]];
            }

            return unzipped_array;
        }

        public static List<VectorUtil.Triangle> VerticesArrayToTrianglesList(this Vector3[] vertices)
        {
            var array = new VectorUtil.Triangle[vertices.Length / 3];
            Parallel.For(0, vertices.Length / 3, i =>
           {
               array[i] = new VectorUtil.Triangle(
                   vertices[3 * i],
                   vertices[3 * i + 1],
                   vertices[3 * i + 2]);
           });
            return array.ToList();
        }

        public static Vector3[] GenRandomSphereVectors(int count)
        {
            var vertices = new Vector3[count];
            for (int i = 0; i < count; i++)
            {
                vertices[i] = Random.rotation * Vector3.forward;
            }
            return vertices;
        }
    }
}

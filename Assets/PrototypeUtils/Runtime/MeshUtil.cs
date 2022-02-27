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
    }
}

using System.Collections.Generic;
using UnityEngine;
namespace PrototypePackages.MiscUtils
{
    public static class MiscUtil
    {
        public static string[] GetHierarchyPath(this Transform transform)
        {
            var path_list = new Stack<string>();
            var cur_transform = transform;

            while (cur_transform != null)
            {
                path_list.Push( cur_transform.name );
                cur_transform = cur_transform.parent;
            }

            return path_list.ToArray();
        }
    }
}

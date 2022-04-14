using System.Collections.Generic;
using UnityEngine;
namespace VivoxPlayground.Lab5_QueryEntityInMono
{
    public class DynamicDictCheck : MonoBehaviour
    {
        public class DynamicKey
        {

            public DynamicKey(bool bool1, int inta)
            {
                this.bool1 = bool1;
                this.inta = inta;
            }
            public bool bool1;
            public int inta;

            public override string ToString()
            {
                return $"{bool1},{inta}";
            }
            // public override int GetHashCode()
            // {
            //     return
            // }
        }

        Dictionary<DynamicKey, string> dict;
        DynamicKey a, b;
        void Start()
        {
            dict = new();
            a = new(true, 1);
            b = new(true, 2);
            dict[a] = "true,1 为key";
            dict[b] = "true,2 为key";
            a.inta = 2;
            Debug.Log( dict[a] );
        }
    }

}

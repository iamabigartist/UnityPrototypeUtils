using System;
using System.Collections.Generic;
using UnityEngine;
namespace Examples.E0_MessLab
{
    public class TryDelegateDict : MonoBehaviour
    {
        public event Action act1;
        public event Action act2;
        Dictionary<MulticastDelegate, MulticastDelegate> table;


        void BindActs()
        {
        }

        void Start()
        {
            table = new();
        }



    }
}

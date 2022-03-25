using PrototypeUtils;
using UnityEngine;
namespace Examples.E0_MessLab
{
    public class TrySingletonMono : SingletonMonoBehaviourRuntime<TrySingletonMono>
    {
        public int value = 1;
        async void Start()
        {
            Debug.Log("AA");
            var me = await IAsyncSingleton<TrySingletonMono>.WaitInstance();
            Debug.Log( me.value );
        }
    }
}

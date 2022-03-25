using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static PrototypeUtils.AsyncUtil;
namespace PrototypeUtils
{
    /// <summary>
    ///     A static singleton constructor and accessor
    ///     <remarks>
    ///         <para>Can only used for class implemented the non arguments new().</para>
    ///         <para><see cref="UnityEngine.MonoBehaviour" /> can't use</para>
    ///     </remarks>
    /// </summary>
    public static class SingletonContainer<T> where T : new()
    {
        static T instance;
        static SingletonContainer()
        {
            instance = new();
        }
        public static T Instance => instance;
    }

    public interface IAsyncSingleton<T> where T : IAsyncSingleton<T>
    {
        static T instance;
        static bool ready => instance != null;
        static async Task<T> WaitInstance()
        {
            return await WaitNullGet( () => instance );
        }
        static async Task WaitReady()
        {
            await WaitUntil( () => instance != null );
        }

    }

    public abstract class SingletonMonoBehaviourRuntime<T> : MonoBehaviour, IAsyncSingleton<T>
        where T : SingletonMonoBehaviourRuntime<T>
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad( this );

            if (IAsyncSingleton<T>.instance == null)
            {
                IAsyncSingleton<T>.instance = (T)this;
            }
            else
            {
                throw new($"{typeof(T)} multiple singleton systems!");
            }
        }
    }

    public abstract class SingletonScriptableObjectAsset<T> : ScriptableObject, IAsyncSingleton<T>
        where T : SingletonScriptableObjectAsset<T>
    {
        public static void LoadSingleton()
        {
            var instance_ = Resources.LoadAll<T>( "" );

            if (instance_.Length != 1)
            {
                throw new($"{typeof(T)} multiple singleton systems!");
            }

            IAsyncSingleton<T>.instance = instance_.Single();
        }
    }



}

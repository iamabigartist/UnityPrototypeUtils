using PrototypeUtils.StateMachine0;
using UnityEngine;
namespace Examples.E5_StateController
{

    public class TestClass1
    {
        public virtual void Act1() { Debug.Log( "aa" ); }

        public void Do()
        {
            Act1();
        }
    }
    public class TestClass2 : TestClass1
    {
        public override void Act1() { Debug.Log( "22" ); }
    }
    public class PubChatMono : MonoBehaviour
    {

        void DebugA(string s)
        {
            Debug.Log( s );
        }
        void DebugB(string s)
        {
            Debug.LogWarning( s );
        }
        void DebugC(string s)
        {
            Debug.LogError( s );
        }
    }
}

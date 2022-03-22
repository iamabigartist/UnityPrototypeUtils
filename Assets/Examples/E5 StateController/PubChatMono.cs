using UnityEngine;
namespace Examples.E5_StateController
{
    public class PubChatMono : MonoBehaviour
    {
        void Start()
        {
        }

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

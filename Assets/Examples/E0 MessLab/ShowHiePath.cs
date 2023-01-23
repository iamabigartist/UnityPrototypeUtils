using PrototypePackages.MiscUtils;
using UnityEngine;
namespace Examples.E0_MessLab
{
    public class ShowHiePath : MonoBehaviour
    {
        void Start()
        {
            Debug.Log( transform.GetHierarchyPath().JoinString( "/" ) );
        }
    }
}

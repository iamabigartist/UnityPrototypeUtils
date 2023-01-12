using UnityEngine;
namespace Examples.E0_MessLab
{
    public class TryCatchRef : MonoBehaviour
    {
        public enum MyEnum
        {
            FFF,
            asda,
            asdas2kjwej2
        }
        void Start()
        {
            Debug.Log( MyEnum.FFF.ToString() );
        }
    }
}

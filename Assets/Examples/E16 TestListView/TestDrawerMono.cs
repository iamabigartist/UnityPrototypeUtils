using UnityEngine;
using UnityEngine.Serialization;
namespace Examples.E16_TestListView
{
public class TestDrawerMono : MonoBehaviour
{
	[FormerlySerializedAs("a")] [SerializeReference]
	public TestTestA TestTestA = new B();
}
}
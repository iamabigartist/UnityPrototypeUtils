using PrototypePackages.MiscUtils.Editor.UIElementUtils;
using UnityEditor;
using UnityEngine;
namespace Examples.E15_TestVEProcessor
{
public class TestVEPMono : MonoBehaviour
{
	SerVETreeGen_UnitySer mgr;
	public Cat cat;
	void Start()
	{
		mgr = new();
		cat = new() { name = "Kitty", age = 3 };
		var ser_prop = new SerializedObject(this).FindProperty("cat");
		var root_node = mgr.GenerateTree(ser_prop);
	}
}
}
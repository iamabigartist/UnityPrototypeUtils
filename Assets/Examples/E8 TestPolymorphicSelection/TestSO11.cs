using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Examples.E8_TestPolymorphicSelection
{
[CreateAssetMenu]
public class TestSO11 : ScriptableObject
{
	public List<Test22> List22;
	public List<TestSO11> List11;

	[ContextMenu("Add22")]
	public void Add22()
	{
		Test22 test22 = CreateInstance<Test22>();
		List22.Add(test22);
		AssetDatabase.AddObjectToAsset(test22, this);
		EditorUtility.SetDirty(this);
	}
}
}
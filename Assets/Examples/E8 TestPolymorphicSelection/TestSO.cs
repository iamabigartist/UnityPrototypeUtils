using System.Collections.Generic;
using UnityEngine;
namespace Examples.E8_TestPolymorphicSelection
{
[CreateAssetMenu(fileName = "TestSO", menuName = "Examples/E8_TestPolymorphicSelection/TestSO")]
public class TestSO : ScriptableObject
{
	public string name;
	public int age;
	public List<TestSO> friends;
}
}
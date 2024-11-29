using System;
using UnityEngine.Serialization;
namespace Examples.E16_TestListView
{
[Serializable]
public class TestTestC
{
	public string name;
	public float age;
}
[Serializable]
public class TestTestA
{
	[FormerlySerializedAs("c")] public TestTestC TestTestC = new();
}
[Serializable]
public class B : TestTestA
{
	public string tag;
}
}
using System.Collections.Generic;
using Sirenix.OdinInspector;
namespace Examples.E15_TestVEProcessor
{
public class PA
{
	public string XX;
	public int YY;
}
public class PB
{
	public int ZZ;
}
public class TestProcessorMechan : SerializedMonoBehaviour
{
	[BoxGroup("Split/BsB")]
	public int AAA;
	[BoxGroup("Split")]
	public string AA;
	[BoxGroup("Split/BsB")]
	public List<int> BB;

	public PA pa;
	public PB pb;

	public void A() {}
}
}
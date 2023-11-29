using UnityEngine;
namespace Examples.E11_TestMatrix
{
[ExecuteAlways]
public class TestTRSMatrix : MonoBehaviour
{
	public Matrix4x4 matrix;
	public Vector3 new_zero;
	void Update()
	{
		new_zero = matrix.MultiplyPoint(Vector3.zero);
	}
}
}
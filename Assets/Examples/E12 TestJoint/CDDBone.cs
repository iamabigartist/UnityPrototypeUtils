using System;
using UnityEngine;
namespace Examples.E12_TestJoint
{
[Serializable]
public class CDDBone
{
	public CDDBone() { weight = 1; }
	public float weight;
	public Transform transform;
	public Vector3 pos => transform.position;
	public Quaternion rot
	{
		get => transform.rotation;
		set => transform.rotation = value;
	}
	public void RotateTo(Quaternion target_rotation, float global_weight)
	{
		rot = Quaternion.Lerp(rot, target_rotation, global_weight * weight);
	}
}
}
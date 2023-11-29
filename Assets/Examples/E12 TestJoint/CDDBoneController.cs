using UnityEngine;
namespace Examples.E12_TestJoint
{
[ExecuteAlways]
public class CDDBoneController : MonoBehaviour
{
	public GameObject bone_prefab;
	public Transform target_transform;
	public float stop_angle = 0.001f;
	public float rot_weight = 1;

	public int bone_list_length_record = 0;
	public CDDBone[] bone_list = { new(), new(), new(), new() };

	bool valid_bones =>
		bone_prefab != null && target_transform != null &&
		bone_list_length_record == bone_list.Length && bone_list.Length >= 2;

	void BoneListUpdate()
	{
		if (bone_prefab == null) return;
		var old_leaf_i = bone_list_length_record - 1;
		var new_leaf_i = bone_list.Length - 1;
		if (bone_list.Length > bone_list_length_record)
		{
			for (int i = old_leaf_i; i < new_leaf_i; i++)
			{
				var cur_parent = i == -1 ? transform : bone_list[i].transform;
				var cur_child = bone_list[i + 1];
				cur_child.transform = Instantiate(bone_prefab,
					Vector3.zero, Quaternion.identity, cur_parent.transform).transform;
				cur_child.transform.localPosition = Vector3.up;
			}
		}
		else if (bone_list.Length < bone_list_length_record)
		{
			var leaf = bone_list[new_leaf_i];
			DestroyImmediate(leaf.transform.GetChild(0).gameObject);
		}

		bone_list_length_record = bone_list.Length;

	}

	void CDD_Update()
	{
		if (!valid_bones) return;
		var leaf_bone = bone_list[bone_list.Length - 1];
		var target_pos = target_transform.position;
		//从末端关节开始更新关节位置，直到根关节
		for (int i_b = bone_list.Length - 2; i_b >= 0; i_b--)
		{
			var cur_bone = bone_list[i_b];
			var dir_target_cur = target_pos - cur_bone.pos;
			var dir_leaf_cur = leaf_bone.pos - cur_bone.pos;
			if (Vector3.Angle(dir_target_cur, dir_leaf_cur) < stop_angle) continue;
			var angle_rotation = Quaternion.FromToRotation(dir_leaf_cur, dir_target_cur);
			var cur_bone_target_rotation = angle_rotation * cur_bone.rot;
			cur_bone.RotateTo(cur_bone_target_rotation, rot_weight);
		}
	}

	void Update()
	{
		BoneListUpdate();
		CDD_Update();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(target_transform.position, transform.position);

		Gizmos.color = Color.white;
		for (int i = 0; i < bone_list.Length - 1; i++)
		{
			var cur_pos = bone_list[i].pos;
			var next_pos = bone_list[i + 1].pos;
			Gizmos.DrawLine(cur_pos, next_pos);
		}
	}




}
}
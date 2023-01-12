using System;
using UnityEngine;
using static Labs.TryImplementFSM.RM_2022_11_2.TestBonePlating.BonePlatingRM.MyEntryData.EntryType;
using static Labs.TryImplementFSM.RM_2022_11_2.TestBonePlating.BonePlatingRM.UsageStateType;
namespace Labs.TryImplementFSM.RM_2022_11_2.TestBonePlating
{

	public class TestBonePlatingRM : MonoBehaviour
	{
		BonePlatingRM rm;
		double cur_remain_time;
		void Start()
		{
			rm = new();
		}

		void Update()
		{
			rm.Run(new() { Type = MachineUpdate });
			cur_remain_time = rm.UsageState.Current switch
			{
				Ready => 0,
				Activated => rm.ActivatedStamp.RemainTime(Time.time, rm.ActivatedDuration),
				Cooling => rm.CooldownStamp.RemainTime(Time.time, rm.CooldownDuration),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		void OnGUI()
		{
			GUI.enabled = rm.UsageState.Current != Cooling;
			if (GUILayout.Button("Damage")) { rm.Run(new() { Type = AfterDamaged }); }
			GUI.enabled = true;
			var box_style = GUI.skin.box;
			box_style.alignment = TextAnchor.UpperLeft;
			GUILayout.Box(JsonUtility.ToJson(new
			{
				rm.ActivatedDuration,
				rm.ActivatedStamp,
				rm.CooldownDuration,
				rm.CooldownStamp,
				rm.PlateCount,
				rm.UsageState.Current
			}, true), box_style);
			GUILayout.Box($"remain_time: {cur_remain_time:##0.###}");

		}
	}
}
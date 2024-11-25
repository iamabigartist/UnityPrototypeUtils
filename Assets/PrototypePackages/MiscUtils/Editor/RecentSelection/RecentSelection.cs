using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
namespace PrototypePackages.MiscUtils.Editor
{
[FilePath("Library/RecentSelection.asset", FilePathAttribute.Location.ProjectFolder)]
public class RecentSelection : ScriptableSingleton<RecentSelection>
{
	public static List<Object> Objects => instance.m_ObjectList;
	[SerializeField] List<Object> m_ObjectList = new();
	[SerializeField] int maxHistorySize = 20; // 限制历史记录条目数量

	void OnEnable()
	{
		TrackSelection();
		Selection.selectionChanged += TrackSelection;
		Debug.Log("RecentSelection Awake");
	}

	void AddCheck(Object selected_obj)
	{

		var list_view = new ListView();
		m_ObjectList.Insert(0, selected_obj);
		if (m_ObjectList.Count > maxHistorySize)
		{
			m_ObjectList.RemoveAt(m_ObjectList.Count - 1);
		}
	}
	void Refresh(int i)
	{
		var selected_obj = m_ObjectList[i];
		m_ObjectList.RemoveAt(i);
		m_ObjectList.Insert(0, selected_obj);
	}

	void TrackSelection()
	{
		var selection_obj_list = Selection.objects;
		foreach (var o in selection_obj_list)
		{
			if (selection_obj_list is null) { continue; }
			if (!m_ObjectList.Contains(o)) { AddCheck(o); }
			else { Refresh(m_ObjectList.IndexOf(o)); }
		}
		Save(true);
	}
}
}
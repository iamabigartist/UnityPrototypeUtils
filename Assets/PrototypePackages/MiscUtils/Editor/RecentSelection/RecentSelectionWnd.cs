using UnityEditor;
using UnityEngine;
using static PrototypePackages.MiscUtils.Editor.RecentSelection;
using static UnityEditor.EditorGUILayout;
namespace PrototypePackages.MiscUtils.Editor
{
public class RecentSelectionWnd : EditorWindow
{
	[MenuItem("Tools/Recent Selections")]
	public static void ShowWindow()
	{
		var wnd = GetWindow<RecentSelectionWnd>("Recent Selections");
	}
	Vector2 s_pos;
	void OnSelectionChange()
	{
		Repaint();
	}
	void OnGUI()
	{
		using (var s = new ScrollViewScope(s_pos))
		{
			s_pos = s.scrollPosition;
			foreach (var obj in Objects)
			{
				if (obj is null) { continue; }
				using (var h = new VerticalScope())
				{
					GUI.enabled = false;
					ObjectField(obj, obj.GetType(), true);
					// if (GUILayout.Button(obj.name, GUILayout.ExpandWidth(true)))
					// {
					// 	EditorGUIUtility.PingObject(obj);
					// }
					GUI.enabled = true;
				}

			}
		}
	}
	void OnInspectorUpdate() { Repaint(); }
}
}
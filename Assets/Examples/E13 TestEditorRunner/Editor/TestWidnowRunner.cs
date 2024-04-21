#if UNITY_EDITOR

using System;
using PrototypePackages.MainThreadExecutor.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public static class TestWindowRunner
{
	static LiteEditorWindow wnd;
	static VisualElement panel;
	static float start_time;
	[MenuItem("Examples/TestWindowRunner")]
	static void ShowExample()
	{
		start_time = Time.realtimeSinceStartup;
		wnd = new("TestWindow", Update);
		wnd.Show();
		panel = new();
		wnd.root.Add(panel);
		panel.style.width = 200;
		panel.style.height = 200;
		panel.style.borderBottomWidth = 20;

	}

	static void Update()
	{
		panel.style.borderBottomColor = new Color((float)Math.Sin(Time.realtimeSinceStartup), 0, 0);
		if (Time.realtimeSinceStartup - start_time > 5)
		{
			wnd.Close();
		}
	}
}

#endif
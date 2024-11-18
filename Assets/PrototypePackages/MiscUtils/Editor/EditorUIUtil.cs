using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace PrototypePackages.MiscUtils.Editor
{
public static class EditorUIUtil
{
	public static VisualElement EncloseByDrawerTypeName(this GUIDrawer d, VisualElement ve)
	{
		var root = new VisualElement();
		root.Add(new Label(d.GetType().FullName)
		{
			style =
			{
				borderLeftColor = Color.gray, borderLeftWidth = 2,
				marginBottom = 3
			}
		});
		root.Add(ve);
		return root;
	}

}
}
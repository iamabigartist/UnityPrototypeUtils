using System.Collections.Generic;
using System.Linq;
using UnityEditor;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public class SerMarkVENode_UnitySerProp : SerMarkVENode<SerializedProperty>
{
	public static SerMarkVENode_UnitySerProp Mark(SerializedProperty ser_prop, string path, List<SerVETreeProcessor_UnitySerProp> modifier_list = null)
	{
		var mark_node = new SerMarkVENode_UnitySerProp
		{
			SerProp = ser_prop.FindPropertyRelative(path),
			ModifierList = modifier_list?.Cast<SerVETreeProcessor<SerializedProperty>>().ToList() ?? new()
		};
		return mark_node;
	}
}
}
using UnityEditor;
using UnityEngine.UIElements;
using UnitySerVE;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public class DefaultSerVETreeProcessor_UnitySerProp : SerVETreeProcessor<SerializedProperty>
{
	public override VENode ProcessTree(SerializedProperty ser_prop, VENode child_root)
	{
		if (ser_prop.isArray)
		{
			var list_ve = new ListView();
			return null;
		}
		else
		{
			var mark_node = new VENode();
			var iterated_prop = ser_prop.Copy();
			foreach (SerializedProperty child_prop in iterated_prop)
			{
				var child_mark_node = new SerMarkVENode<SerializedProperty> { SerProp = child_prop };
				mark_node.Children.Add(child_mark_node);
			}
			return mark_node;
		}
	}
}
}
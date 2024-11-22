using UnityEditor;
using UnityEditor.UIElements;
using UnitySerVE;
using static UnityEditor.SerializedPropertyType;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public class PrimitiveSerProcessor : SerVETreeProcessor_UnitySerProp
{
	public override bool CanProcess(SerializedProperty ser_prop)
	{
		var t = ser_prop.propertyType;
		return t is not (Generic or ExposedReference or ManagedReference or ObjectReference);
	}
	public override VENode ProcessTree(SerializedProperty ser_prop, VENode child_root)
	{
		return new(new PropertyField(ser_prop));
	}
}
}
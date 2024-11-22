using UnityEditor;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public abstract class SerVETreeProcessor_UnitySerProp : SerVETreeProcessor<SerializedProperty>
{
	public abstract bool CanProcess(SerializedProperty ser_prop);
}
}
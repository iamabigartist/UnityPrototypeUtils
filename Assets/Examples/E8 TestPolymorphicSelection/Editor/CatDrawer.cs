using PrototypePackages.MiscUtils.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Examples.E8_TestPolymorphicSelection.Editor
{
[CustomPropertyDrawer(typeof(Cat))]
public class CatDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		Debug.Log(property);
		VisualElement root = new PropertyField(property);
		root = this.EncloseByDrawerTypeName(root);
		return root;
	}
}
}
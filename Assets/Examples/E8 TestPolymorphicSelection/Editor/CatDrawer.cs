using PrototypePackages.MiscUtils.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
namespace Examples.E8_TestPolymorphicSelection.Editor
{
[CustomPropertyDrawer(typeof(Cat))]
public class CatDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		VisualElement root = new VisualElement() { style = { flexDirection = FlexDirection.Row } };
		root.Add(new PropertyField(property.FindPropertyRelative(nameof(Cat.name))));
		root.Add(new PropertyField(property.FindPropertyRelative(nameof(Cat.color))));
		root.Add(new PropertyField(property.FindPropertyRelative(nameof(Cat.attack)))
		{
			
		});
		// root = this.EncloseByDrawerTypeName(root);
		return root;
	}
}
}
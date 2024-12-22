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
		VisualElement root = new VisualElement() { style = { flexDirection = FlexDirection.Column } };
		var name = new PropertyField(property.FindPropertyRelative(nameof(Cat.name)));
		var name1 = new PropertyField(property.FindPropertyRelative(nameof(Cat.name)));
		var name2 = new PropertyField(property.FindPropertyRelative(nameof(Cat.name)));
		var box = new Box();
		var box1 = new Box();
		box.Add(name);
		box1.Add(name1);
		root.Add(new PropertyField(property.FindPropertyRelative(nameof(Cat.color))));
		root.Add(new PropertyField(property.FindPropertyRelative(nameof(Cat.attack))));
		root.Add(box);
		root.Add(box1);

		root.Add(new PropertyField(property.FindPropertyRelative(nameof(Cat.friends))));

		// root = this.EncloseByDrawerTypeName(root);
		return root;
	}
}
}
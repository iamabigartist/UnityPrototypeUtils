using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
namespace Examples.E16_TestListView
{

public class AA<T>
{
	public class BBDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var r = new VisualElement();
			var c = property.FindPropertyRelative(nameof(TestTestA.TestTestC));
			r.Add(new PropertyField(c));
			return r;
		}
	}
}

[CustomPropertyDrawer(typeof(TestTestA))]
public class ADrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		var r = new VisualElement();
		var c = property.FindPropertyRelative(nameof(TestTestA.TestTestC));
		r.Add(new PropertyField(c));
		return r;
	}
}

[CustomPropertyDrawer(typeof(TestTestA))]
public class BDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		var r = new VisualElement();
		var c = property.FindPropertyRelative(nameof(TestTestA.TestTestC));
		var tag = property.FindPropertyRelative(nameof(B.tag));
		r.Add(new PropertyField(c));
		r.Add(new PropertyField(tag));
		return r;
	}
}

[CustomPropertyDrawer(typeof(TestTestC))]
public class CDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		var r = new VisualElement()
		{
			style =
			{
				flexDirection = FlexDirection.Row
			}
		};
		var name = property.FindPropertyRelative(nameof(TestTestC.name));
		var age = property.FindPropertyRelative(nameof(TestTestC.age));
		r.Add(new PropertyField(name));
		r.Add(new PropertyField(age));
		return r;
	}
}
}
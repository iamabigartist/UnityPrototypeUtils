using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
namespace PrototypePackages.MiscUtils.Editor
{


public static class UnityUISerializeUtil
{
	/// Creates instance of passed type and assigns it to managed reference
	public static void AssignNewInstanceOfTypeToManagedReference(this SerializedProperty serializedProperty, object obj)
	{
		// serializedProperty.serializedObject.Update();
		serializedProperty.managedReferenceValue = obj;
		serializedProperty.serializedObject.ApplyModifiedProperties();
	}

	public static (string AssemblyName, string ClassName) GetSplitNamesFromTypename(string typename)
	{
		if (string.IsNullOrEmpty(typename))
			return ("", "");
		var typeSplitString = typename.Split(char.Parse(" "));
		var typeClassName = typeSplitString[1];
		var typeAssemblyName = typeSplitString[0];
		return (typeAssemblyName, typeClassName);
	}

	public static Type GetTypeFromTypename(this SerializedProperty serializedProperty)
	{
		var names = GetSplitNamesFromTypename(serializedProperty.managedReferenceFieldTypename);
		var realType = Type.GetType($"{names.ClassName}, {names.AssemblyName}");
		return realType;
	}
}

[CustomPropertyDrawer(typeof(PolymorphicSelectAttribute))]
public class PolymorphicSelectDrawer : PropertyDrawer
{
	List<Type> instance_types;
	List<string> type_names;
	IInstanceGenerator Generator;

	int CurChoice(SerializedProperty property)
	{
		return instance_types.IndexOf(property.managedReferenceValue?.GetType());
	}

	void RefreshObject(SerializedProperty property, int type_index)
	{
		var obj = Generator.Generate(instance_types[type_index]);
		property.AssignNewInstanceOfTypeToManagedReference(obj);
	}

	// bool inited;
	void Init(SerializedProperty property)
	{
		// if (inited) return;
		var parent_type = property.GetTypeFromTypename();
		instance_types = TypeCache.GetTypesDerivedFrom(parent_type).Where(Type => !Type.IsAbstract).ToList();
		type_names = instance_types.Select(Type => Type.Name).ToList();
		var atr = fieldInfo.GetCustomAttribute<PolymorphicSelectAttribute>();
		Generator = (IInstanceGenerator)Activator.CreateInstance(atr.InstanceGenType);
		var current_type = property.managedReferenceValue?.GetType();
		// inited = true;
	}


	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		Init(property);
		var root = new VisualElement();
		root.Add(CreatePopUp(property));
		root.Add(new PropertyField(property));
		root = this.EncloseByDrawerTypeName(root);
		return root;
	}

	VisualElement CreatePopUp(SerializedProperty property)
	{
		var pop_up = new DropdownField("type", type_names, CurChoice(property));
		pop_up.RegisterValueChangedCallback(Evt =>
		{
			var new_choice = Evt.newValue;
			if (type_names.IndexOf(new_choice) != CurChoice(property))
			{
				RefreshObject(property, type_names.IndexOf(new_choice));
			}
		});
		return pop_up;
	}

	// void OnTypeSelectionPopUp(Rect position, SerializedProperty property, GUIContent label)
	// {
	// 	var line_rect = position.UpPart(singleLineHeight);
	// 	var new_choice = EditorGUI.Popup(line_rect, label.text, CurChoice(property), type_names);
	// 	if (new_choice != CurChoice(property))
	// 	{
	// 		RefreshObject(property, new_choice);
	// 	}
	// }

	// public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	// {
	// 	return EditorGUI.GetPropertyHeight(property, label, true);
	// }
	//
	// public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	// {
	// 	if (!inited) { Init(property); }
	// 	EditorGUI.BeginProperty(position, label, property);
	// 	OnTypeSelectionPopUp(position, property, label);
	// 	EditorGUI.PropertyField(position, property, GUIContent.none, true);
	// 	EditorGUI.EndProperty();
	// }
}
}
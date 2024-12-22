using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PrototypePackages.MiscUtils.Editor.TypeSelector;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace PrototypePackages.MiscUtils.Editor
{



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
		// root = this.EncloseByDrawerTypeName(root);
		return root;
	}

	VisualElement CreatePopUp(SerializedProperty property)
	{
		var pop_up = new DropdownField("Type", type_names, CurChoice(property));
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
}
}
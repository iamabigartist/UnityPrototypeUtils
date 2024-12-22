using System;
using System.Text;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
namespace PrototypePackages.MiscUtils.Editor.TypeSelector
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
	
	static void PrintResults(ISearchList results)
	{
		var sb = new StringBuilder();
		sb.AppendLine($"Found {results.Count} results:");
		Debug.Log(sb.ToString());
	}
	
	/// <summary>
	/// Find all entries which contain the text "hello" in English.
	/// </summary>
	[MenuItem("Localization Samples/Search/Find Hello")]
	public static void FindHello()
	{
		var search = SearchService.Request("st: tr(en):hello", SearchFlags.Synchronous);
		PrintResults(search);
	}
}
}
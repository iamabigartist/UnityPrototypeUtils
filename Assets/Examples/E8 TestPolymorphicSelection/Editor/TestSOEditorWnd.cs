using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
namespace Examples.E8_TestPolymorphicSelection.Editor
{
public class TestSOEditorWnd : EditorWindow
{
	[MenuItem("Examples/Test Polymorphic Selection/TestSOEditorWnd")]
	static void Open()
	{
		var wnd = GetWindow<TestSOEditorWnd>();
	}

	public StyleSheet m_USS;

	SerializedObject m_SerializedObject;
	List<TestSO> m_TestSOList = new();
	void Awake() {}

	Box m_InspectorElementBox;
	void CreateGUI()
	{
		m_USS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Examples/E8 TestPolymorphicSelection/TestEditorStyle.uss");
		m_InspectorElementBox = new();
		rootVisualElement.Add(m_InspectorElementBox);
		rootVisualElement.styleSheets.Add(m_USS);
	}

	void OnSelectionChange()
	{
		m_InspectorElementBox.Query<InspectorElement>().ForEach(e => e.RemoveFromHierarchy());
		var serObj = new SerializedObject(Selection.GetFiltered(typeof(TestSO), SelectionMode.DeepAssets | SelectionMode.Assets));
		m_InspectorElementBox.Add(new InspectorElement(serObj));
	}
}
}
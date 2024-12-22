using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
namespace Examples.E8_TestPolymorphicSelection.Editor
{
public class ViewUXMLWnd : EditorWindow
{
	[MenuItem("Examples/E8 TestPolymorphicSelection/ViewUXMLWnd")]
	static void ShowWindow()
	{
		GetWindow<ViewUXMLWnd>();
	}
	public VisualTreeAsset m_UXML;
	public StyleSheet m_USS;
	public bool m_IsDirty;
	void CreateGUI()
	{
		var root = rootVisualElement;
		root.dataSource = this;
		var uxml_field = new ObjectField { objectType = typeof(VisualTreeAsset) };
		uxml_field.SetBinding("value",
			new DataBinding
			{
				dataSourcePath = new("m_UXML"),
				bindingMode = BindingMode.ToSource
			});
		uxml_field.RegisterValueChangedCallback(_ => m_IsDirty = true);

		var uss_field = new ObjectField { objectType = typeof(StyleSheet) };
		uss_field.SetBinding("value",
			new DataBinding
			{
				dataSourcePath = new("m_USS"),
				bindingMode = BindingMode.ToSource
			});
		uss_field.RegisterValueChangedCallback(_ => m_IsDirty = true);
		root.Add(uxml_field);
		root.Add(uss_field);
		root.Add(new Box()
		{
			name = "RenderUXMLBox",
			style = { flexGrow = 1 }
		});
	}

	void RefreshUXML()
	{
		rootVisualElement.Q<VisualElement>("RenderUXMLBox").Clear();
		if (m_UXML != null)
		{
			var uxml_ve = m_UXML.CloneTree();
			if (m_USS != null) { uxml_ve.styleSheets.Add(m_USS); }
			rootVisualElement.Q<VisualElement>("RenderUXMLBox").Add(uxml_ve);
		}
	}

	void OnInspectorUpdate()
	{
		if (m_IsDirty)
		{
			RefreshUXML();
			m_IsDirty = false;
		}
	}
}
}
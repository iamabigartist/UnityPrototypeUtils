using PrototypePackages.MainThreadExecutor.Scripts;
using UnityEditor;
using UnityEngine.UIElements;
namespace PrototypePackages.MainThreadExecutor.Editor
{
public class LiteEditorWindow : Executor
{
	public EditorWindow window;
	public VisualElement root;
	EditorApplication.CallbackFunction on_update;
	public LiteEditorWindow(string title, EditorApplication.CallbackFunction on_update = default)
	{
		window = EditorWindow.CreateWindow<EditorWindow>(title);
		root = window.rootVisualElement;
		this.on_update = on_update;
		EditorApplication.update += Update;
	}
	void Update()
	{
		on_update?.Invoke();
		UpdaterUpdate();
	}
	public void Show() => window.Show();
	public void Close()
	{
		window.Close();
		EditorApplication.update -= Update;
	}
}
}
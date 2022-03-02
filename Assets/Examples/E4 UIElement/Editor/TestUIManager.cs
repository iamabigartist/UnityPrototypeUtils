using PrototypeUtils;
using UnityEditor;
using UnityEngine.UIElements;
namespace Examples.E4_UIElement.Editor
{
    public class TestUIManager : EditorWindow
    {
        [MenuItem( "Labs/UnityEngine.UIElements/TestUIManager" )]
        static void ShowWindow()
        {
            var window = GetWindow<TestUIManager>();
            window.titleContent = new("TestUIManager");
            window.Show();
        }

        UIManager manager;
        string text1;

        void OnEnable()
        {
            manager = new(rootVisualElement);
        }

        void CreateGUI()
        {
            rootVisualElement.Set4Padding( 7 );
            var text_f1 = manager.Add<TextField>( new("Text Field") );
            manager.ApplyTo<TextField, string>(
                text_f1, e => text1 = e.newValue );
            var l1 = manager.Add<Label>( new("Label1") );
            manager.UpdateFrom( l1, () => $"Label: {text1}" );
        }

        void OnInspectorUpdate()
        {
            manager.Update();
        }
    }
}

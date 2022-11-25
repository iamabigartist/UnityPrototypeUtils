using System;
using PrototypePackages.PrototypeUtils.Runtime;
using UnityEditor;
using UnityEngine;
namespace Examples.E3_Round.Editor
{
    public class TestRound : EditorWindow
    {
        [MenuItem( "Examples/Examples.E3_Round.Editor/TestRound" )]
        static void ShowWindow()
        {
            var window = GetWindow<TestRound>();
            window.titleContent = new GUIContent( "TestRound" );
            window.Show();
        }

        double source;

        Vector3 v;
        Vector3 up;
        Vector3 forward;

        void OnEnable()
        {
            v = (0.5f, 0f, 0f).V();
        }

        void OnGUI()
        {
            source = EditorGUILayout.DoubleField( "source", source );
            EditorGUILayout.DoubleField( "rounded", Math.Round( source, 1 ) );

            v = EditorGUILayout.Vector3Field( "v", v );
            var v_rotated = Quaternion.LookRotation( Vector3.left, Vector3.up ) * v;
            EditorGUILayout.Vector3Field( "rotated", v_rotated.Round( 1 ) );
        }
    }
}

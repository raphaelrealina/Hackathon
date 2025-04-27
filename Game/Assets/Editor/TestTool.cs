using UnityEditor;
using UnityEngine;

public class TestTool : EditorWindow
{
    [MenuItem("Tools/Test Tool")]
    static void ShowWindow()
    {
        Debug.Log("Editor script is working!");
        GetWindow<TestTool>("Test Tool");
    }

    void OnGUI()
    {
        GUILayout.Label("Hello, Editor!");
    }
}
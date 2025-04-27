using UnityEditor;
using UnityEngine;

public class MaterialBatchApplier : EditorWindow
{
    private Material targetMaterial;
    
    [MenuItem("Tools/Batch Apply Material")]
    public static void ShowWindow()
    {
        GetWindow<MaterialBatchApplier>("Material Applier");
    }

    void OnGUI()
    {
        targetMaterial = (Material)EditorGUILayout.ObjectField("Material to Apply", targetMaterial, typeof(Material), false);
        
        if (GUILayout.Button("Apply to Selected Objects") && targetMaterial != null)
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.sharedMaterial = targetMaterial;
                }
            }
        }
    }
}
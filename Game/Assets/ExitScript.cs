using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitScript : MonoBehaviour
{
    // This function can be linked to the button's OnClick()
    public void ExitGame()
    {
        Debug.Log("VRExitButton: Exit triggered.");

        #if UNITY_EDITOR
        // If running inside Unity Editor, stop play mode
        EditorApplication.isPlaying = false;
        #else
        // If running a build, quit the application
        Application.Quit();
        #endif
    }
}


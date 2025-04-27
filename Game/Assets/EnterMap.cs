using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMap : MonoBehaviour
{
    // Name of the scene you want to load
    [SerializeField] private string sceneToLoad; // add =scene;

    // This function can be linked to the button's OnClick()
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"EnterMap: Loading scene '{sceneToLoad}'...");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("EnterMap: Scene name not set!");
        }
    }
}

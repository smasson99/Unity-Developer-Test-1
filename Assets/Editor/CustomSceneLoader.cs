using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CustomSceneLoader : EditorWindow
{
    [MenuItem("Tools/Scene Loader")]
    private static void CreateCustomSceneLoader()
    {
        GetWindow<CustomSceneLoader>();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("SplashScreen", GUILayout.Width(140), GUILayout.Height(25)))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Scenes/SplashScreen.unity");
        }
        
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("MainMenu", GUILayout.Width(140), GUILayout.Height(25)))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
        }
        
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Game", GUILayout.Width(140), GUILayout.Height(25)))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Scenes/Game/Game.unity");
        }
        
        GUILayout.EndHorizontal();
    }
}

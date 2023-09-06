using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor.EditorUtils
{
    [InitializeOnLoad]
    public class StartFromAnyScene : EditorWindow
    {
        private static string _startingSceneName;
        private static string _currentSceneName;
        private static readonly string editorPrefsKey = "StartingScene";
        private static readonly string editorCurrentSceneKey = "CurrentSceneKey";

        static StartFromAnyScene()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            _startingSceneName = EditorPrefs.GetString(editorPrefsKey);
            Debug.Log(_startingSceneName);
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                _currentSceneName = SceneManager.GetActiveScene().name;
                EditorPrefs.SetString(editorCurrentSceneKey, _currentSceneName);
                Debug.Log(" current scene name : " + _currentSceneName);
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene("Assets/Scenes/" + _startingSceneName + ".unity"); 
            }
            else if (state == PlayModeStateChange.EnteredEditMode)
            {
                Debug.Log(_currentSceneName);
                _currentSceneName = EditorPrefs.GetString(editorCurrentSceneKey);
                EditorSceneManager.OpenScene("Assets/Scenes/" + _currentSceneName + ".unity");
                Debug.Log("else if currentscenename :" + _currentSceneName);
            }
        }

        [MenuItem("Custom/Start With Specific Scene")]
        private static void AlwaysStartWithSceneMenuOption()
        {
            _startingSceneName = EditorUtility.OpenFilePanel("Select Starting Scene", "Assets/Scenes", "unity");
            if (!string.IsNullOrEmpty(_startingSceneName))
            {
                int index = _startingSceneName.IndexOf("Assets/Scenes/", StringComparison.Ordinal);
                if (index >= 0)
                {
                    _startingSceneName = _startingSceneName.Substring(index + "Assets/Scenes/".Length);
                    _startingSceneName = _startingSceneName.Replace(".unity", "");
                    
          
                    EditorPrefs.SetString(editorPrefsKey, _startingSceneName);
                    Debug.Log("Start set to: " + _startingSceneName);
                }
            }
        }
    }
}

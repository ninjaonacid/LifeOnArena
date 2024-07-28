using TMPro;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.EditorWindows
{
    public class ChangeAllFonts : EditorWindow
    {
        private TMP_FontAsset newFont;
    
        [MenuItem("Tools/Change All Fonts")]
        public static void ShowWindow()
        {
            GetWindow<ChangeAllFonts>("Change All Fonts");
        }
    
        void OnGUI()
        {
            GUILayout.Label("Select new font asset:", EditorStyles.boldLabel);
            newFont = (TMP_FontAsset)EditorGUILayout.ObjectField(newFont, typeof(TMP_FontAsset), false);
    
            if (GUILayout.Button("Change Fonts"))
            {
                if (newFont != null)
                {
                    ChangeAllFontsInProject();
                    ChangeAllFontsInScene();
                }
                else
                {
                    Debug.LogError("Please select a font asset before changing fonts.");
                }
            }
        }
    
        void ChangeAllFontsInProject()
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    TextMeshProUGUI[] texts = prefab.GetComponentsInChildren<TextMeshProUGUI>(true);
                    TextMeshPro[] textMeshes = prefab.GetComponentsInChildren<TextMeshPro>(true);
    
                    bool prefabModified = false;
    
                    foreach (TextMeshProUGUI text in texts)
                    {
                        if (text.font != newFont)
                        {
                            text.font = newFont;
                            prefabModified = true;
                        }
                    }
    
                    foreach (TextMeshPro textMesh in textMeshes)
                    {
                        if (textMesh.font != newFont)
                        {
                            textMesh.font = newFont;
                            prefabModified = true;
                        }
                    }
    
                    if (prefabModified)
                    {
                        PrefabUtility.SavePrefabAsset(prefab);
                    }
                }
            }
        }
    
        void ChangeAllFontsInScene()
        {
            TextMeshProUGUI[] textsInScene = FindObjectsOfType<TextMeshProUGUI>();
            TextMeshPro[] textMeshesInScene = FindObjectsOfType<TextMeshPro>();
    
            foreach (TextMeshProUGUI text in textsInScene)
            {
                if (text.font != newFont)
                {
                    Undo.RecordObject(text, "Change Font");
                    text.font = newFont;
                }
            }
    
            foreach (TextMeshPro textMesh in textMeshesInScene)
            {
                if (textMesh.font != newFont)
                {
                    Undo.RecordObject(textMesh, "Change Font");
                    textMesh.font = newFont;
                }
            }
        }
    }
}

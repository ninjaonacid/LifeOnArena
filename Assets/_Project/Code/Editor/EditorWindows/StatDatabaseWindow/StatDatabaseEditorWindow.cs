using Code.StaticData.StatSystem;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Editor.EditorWindows.StatDatabaseWindow
{
    public class StatDatabaseEditorWindow : EditorWindow
    {

        private StatDatabase _dataBase;

        [MenuItem("Window/StatSystem/StatDatabase")]
        public static void ShowWindow()
        {
            StatDatabaseEditorWindow window = GetWindow<StatDatabaseEditorWindow>();

            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent("StatDatabase");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceId) is StatDatabase)
            {
                ShowWindow();
                return true;
            }

            return false;
        }

        private void OnSelectionChanged()
        {
            _dataBase = Selection.activeObject as StatDatabase;
        }

        public void CreateGUI()
        {
            OnSelectionChanged();
   
            VisualElement root = rootVisualElement;
 
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_Project/Code/Editor/EditorWindows/StatDatabaseWindow/StatDatabaseEditorWindow.uxml");
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_Project/Code/Editor/EditorWindows/StatDatabaseWindow/StatDatabaseEditorWindow.uss");
            root.styleSheets.Add(styleSheet);
        }
    }
}
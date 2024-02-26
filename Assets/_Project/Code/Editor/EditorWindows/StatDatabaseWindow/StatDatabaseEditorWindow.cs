using Code.Runtime.ConfigData.StatSystem;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Editor.EditorWindows.StatDatabaseWindow
{
    public class StatDatabaseEditorWindow : EditorWindow
    {
        private StatDatabase _dataBase;
        private StatCollectionEditor _currentCollection;
        
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
 
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/_Project/Code/Editor/EditorWindows/StatDatabaseWindow/StatDatabaseEditorWindow.uxml");
            
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Assets/_Project/Code/Editor/EditorWindows/StatDatabaseWindow/StatDatabaseEditorWindow.uss");
            
            root.styleSheets.Add(styleSheet);

            StatCollectionEditor stats = root.Q<StatCollectionEditor>("stats");
            stats.Initialize(_dataBase, _dataBase.StatDefinitions);
            Button statsTab = root.Q<Button>("stats-tab");
            statsTab.clicked += () =>
            {
                _currentCollection.style.display = DisplayStyle.None;
                stats.style.display = DisplayStyle.Flex;
                _currentCollection = stats;
            };

            StatCollectionEditor primaryStats = root.Q<StatCollectionEditor>("primary-stats");
            primaryStats.Initialize(_dataBase, _dataBase.PrimaryStats);
            Button primaryStatsTab = root.Q<Button>("PrimaryStatsTab");
            primaryStatsTab.clicked += () =>
            {
                _currentCollection.style.display = DisplayStyle.None;
                primaryStats.style.display = DisplayStyle.Flex;
                _currentCollection = primaryStats;
            };

            StatCollectionEditor attributes = root.Q<StatCollectionEditor>("attributes");
            attributes.Initialize(_dataBase, _dataBase.AttributeDefinitions);
            Button attributesTab = root.Q<Button>("AttributesTab");
            attributesTab.clicked += () =>
            {
                _currentCollection.style.display = DisplayStyle.None;
                attributes.style.display = DisplayStyle.Flex;
                _currentCollection = attributes;
            };

            _currentCollection = stats;

        }
    }
}
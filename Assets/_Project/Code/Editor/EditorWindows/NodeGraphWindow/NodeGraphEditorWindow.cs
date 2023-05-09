using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Editor.EditorWindows.NodeGraphWindow
{
    public class NodeGraphEditorWindow : EditorWindow
    {
        [MenuItem("Window/UI Toolkit/NodeGraphEditorWindow")]
        public static void ShowWindow()
        {
            NodeGraphEditorWindow window = GetWindow<NodeGraphEditorWindow>();
            window.titleContent = new GUIContent("NodeGraphEditorWindow");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
        
            VisualElement label = new Label("Hello World! From C#");
            root.Add(label);
        
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/_Project/Code/Editor/EditorWindows/NodeGraphWindow/NodeGraphEditorWindow.uxml");
            
            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);
        
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Assets/_Project/Code/Editor/EditorWindows/NodeGraphWindow/NodeGraphEditorWindow.uss");
        
        }
    }
}
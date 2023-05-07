using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Editor.EditorWindows
{
    public class ScriptableObjectCollectionEditor<T> : VisualElement where T : ScriptableObject
    {
        protected ScriptableObject _target;
        protected List<T> _items;
        private ListView _listView;
        private Button _createButton;
        private List<T> _filteredListView;
        private InspectorElement _inspector;
        private ToolbarSearchField _toolbarSearchField;
        private PropertyField _propertyField;

        public ScriptableObjectCollectionEditor()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/_Project/Code/Editor/EditorWindows/ScriptableObjectCollectionEditor.uxml");
            visualTree.CloneTree(this);

            _inspector = this.Q<CustomInspector>();
            _listView = this.Q<ListView>();
            _createButton = this.Q<Button>("CreateButton");
            _toolbarSearchField = this.Q<ToolbarSearchField>();
            _propertyField = this.Q<PropertyField>();


        }

        //public void Initialize(ScriptableObject target, List<T> items)
        //{
        //    _target = target;
        //    _items = items;
        //    InitializeInternal();
        //}

        //private void InitializeInternal()
        //{
        //    Func<VisualElement> makeItem = () => new Label();
        //    _listView.makeItem = makeItem;
        //    _listView.onSelectionChange += objects =>
        //    {
        //        T item = objects.First() as T;
        //        Select(item);
        //    };

        //    Action<VisualElement, int> bindItem = (element, index) =>
        //    {
        //        Label label = element as Label;
        //        label.AddManipulator(new ContextualMenuManipulator(evt =>
        //        {
        //            evt.menu.AppendAction("Duplicate", action =>
        //            {
        //                Duplicate(_filteredListView);
        //            });
        //            evt.menu.AppendAction("Remove", action =>
        //            {
        //                Remove(_filteredListView[index]);
        //            });

        //        }));

        //        SerializedObject serializedObject = new SerializedObject(_filteredListView[index]);
        //        SerializedProperty serializedProperty = serializedObject.FindProperty("_name");
        //        label.BindProperty(serializedProperty);
        //    };

        //    _listView.bindItem = bindItem;
        //    _listView.itemsSource = _filteredListView = _items;

        //    _createButton.clicked += Create;

        //    _toolbarSearchField.RegisterCallback<ChangeEvent<string>>(evt =>
        //    {
        //        _listView.itemsSource = _filteredListView = _items
        //            .Where(item => item.name.StartsWith(evt.newValue, StringComparison.OrdinalIgnoreCase)).ToList();
        //        _listView.Rebuild();
        //    });
        //}
    }
}

using System;
using System.Linq;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;

namespace Code.Editor.ScriptableObjects
{
    [CustomEditor(typeof(GameplayEffectBlueprint))]
    public class GameplayEffectBlueprintEditor : OdinEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            root.Add(CreateProperties());
            root.Add(CreateModifierList());

            return root;
        }

        protected virtual VisualElement CreateProperties()
        {
            var root = new VisualElement();
            
            root.Add(new PropertyField(serializedObject.FindProperty("EffectDurationType")));
            return root;
        }

        protected virtual VisualElement CreateModifierList()
        {
            var root = new VisualElement();

            ListView modifiers = new ListView
            {
                bindingPath = "Modifiers",
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                reorderable = true,
                showFoldoutHeader = true,
                showAddRemoveFooter = true,
                headerTitle = "Modifiers"
            };
            
            modifiers.Bind(serializedObject);
            Button addButton = modifiers.Q<Button>("unity-list-view__add-button");
            Button removeButton = modifiers.Q<Button>("unity-list-view__remove-button");
            addButton.clicked += AddButtonClicked;
            removeButton.clicked += RemoveButtonClicked;
            root.Add(modifiers);
            return root;
        }

        private void RemoveButtonClicked()
        {
            RemoveItem();
        }

        private void AddButtonClicked()
        {
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(type => typeof(StatModifierBlueprint).IsAssignableFrom(type)
                               && type.IsClass && !type.IsAbstract).ToArray();

            if (types.Length > 1)
            {
                GenericMenu menu = new GenericMenu();

                foreach (var type in types)
                {
                    menu.AddItem(new GUIContent(type.Name), false, () => CreateItem(type));
                }
                
                menu.ShowAsContext();
            }
            else
            {
                CreateItem(types[0]);
            }
        }

        private void CreateItem(Type type)
        {
            StatModifierBlueprint item = ScriptableObject.CreateInstance(type) as StatModifierBlueprint;
            item.name = "Modifier";
            AssetDatabase.AddObjectToAsset(item, target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SerializedProperty modifiers = serializedObject.FindProperty("Modifiers");
            modifiers.GetArrayElementAtIndex(modifiers.arraySize - 1).objectReferenceValue = item;
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        private void RemoveItem()
        {
           var path = AssetDatabase.GetAssetPath(target);
           var items = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

           if (items.Length > 0)
           {
               var assetToRemove = items[0];
               AssetDatabase.RemoveObjectFromAsset(assetToRemove);
               AssetDatabase.SaveAssets();
           }
           
        }
    }
}

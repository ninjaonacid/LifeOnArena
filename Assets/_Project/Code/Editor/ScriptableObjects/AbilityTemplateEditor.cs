using Code.Runtime.Modules.AbilitySystem;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Code.Editor.ScriptableObjects
{
    [CustomEditor(typeof(AbilityBase), true)]
    public class AbilityTemplateEditor : OdinEditor
    {
        // public override VisualElement CreateInspectorGUI()
        // {
        //     base.CreateInspectorGUI();
        //     var root = new VisualElement();
        //     
        //     root.Add(CreatePropertyFields());
        //     root.Add(CreateStatusEffectCollection());
        //     return root;
        // }
        //
        // protected virtual VisualElement CreatePropertyFields()
        // {
        //     var root = new VisualElement();
        //     root.Add(new PropertyField(serializedObject.FindProperty("Identifier")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("Icon")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("IsCastAbility")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("Cooldown")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("CurrentCooldown")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("ActiveTime")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("CurrentActiveTime")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("State")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("VfxData")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("ProjectileLifetime")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("Radius")));
        //     root.Add(new PropertyField(serializedObject.FindProperty("CastDistance")));
        //     
        //     return root;
        // }
        //
        // protected virtual VisualElement CreateStatusEffectCollection()
        // {
        //     var root = new VisualElement();
        //
        //     ListView modifiers = new ListView
        //     {
        //         bindingPath = "_statusEffects",
        //         virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
        //         reorderable = true,
        //         showFoldoutHeader = true,
        //         showAddRemoveFooter = true,
        //         headerTitle = "Modifiers"
        //     };
        //     modifiers.Bind(serializedObject);
        //     root.Add(modifiers);
        //     return root;
        // }
        
        
    }
}
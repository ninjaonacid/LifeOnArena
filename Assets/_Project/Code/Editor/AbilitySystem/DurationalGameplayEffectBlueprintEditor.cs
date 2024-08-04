using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Code.Runtime.Modules.AbilitySystem.Editor
{
    [CustomEditor(typeof(DurationalGameplayEffectBlueprint))]
    public class DurationalGameplayEffectBlueprintEditor : GameplayEffectBlueprintEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            root.Add(CreateProperties());
            root.Add(CreateModifierList());
            root.Add(CreateTagsList());

            return root;
        }

        protected override VisualElement CreateProperties()
        {
            VisualElement root = base.CreateProperties();
            
            root.Add(new PropertyField(serializedObject.FindProperty("_duration")));
            root.Add(new PropertyField(serializedObject.FindProperty("_tickRate")));

            return root;
        }
        
    }
}

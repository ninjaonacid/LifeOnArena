using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Code.Editor.ScriptableObjects
{
    [CustomEditor(typeof(GameplayEffectBlueprint), true)]
    public class StatusEffectTemplateEditor : OdinEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            return base.CreateInspectorGUI();

            var root = new VisualElement();
            
            root.Add(DrawProperties());
        }


        protected virtual VisualElement DrawProperties()
        {
            var root = new VisualElement();
            
            root.Add(new PropertyField(serializedObject.FindProperty("EffectDurationType")));

            return root;
        }
    }
}

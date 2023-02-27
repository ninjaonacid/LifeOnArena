using System;
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
    public class ScriptableObjectIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }

            EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = true;
        }
    }
}

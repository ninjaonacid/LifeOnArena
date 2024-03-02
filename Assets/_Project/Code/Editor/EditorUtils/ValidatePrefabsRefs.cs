using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Editor.EditorUtils
{
    public class PrefabFieldsValidator : EditorWindow
    {
        [MenuItem("Tools/Check Prefab Fields")]
        public static void CheckPrefabs()
        {
            string[] prefabPaths = GetAllPrefabPaths();

            foreach (string prefabPath in prefabPaths)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

                if (prefab != null)
                {
                    Component[] components = prefab.GetComponents<Component>();

                    foreach (Component component in components)
                    {
                        if (!component)
                        {
                            Debug.LogWarning($"Component missing in prefab '{prefab.name}' Prefab path {prefabPath}", prefab.gameObject);
                            continue;
                        }

                        SerializedObject serializedObject = new SerializedObject(component);
                        SerializedProperty property = serializedObject.GetIterator();

                        while (property.NextVisible(true))
                        {
                            if (IsValidReferenceType(property) &&
                                property.objectReferenceValue == null)
                            {
                                Type componentType = GetFieldType(property);

                                if (componentType != null && IsComponentType(componentType))
                                {
                                    Component missingComponent;

                                    if (prefab.TryGetComponent(componentType, out missingComponent))
                                    {
                                        property.objectReferenceValue = missingComponent;
                                    }

                                    else if (!missingComponent)
                                    {
                                        missingComponent = prefab.GetComponentInChildren(componentType);
                                    }

                                    else if (!missingComponent)
                                    {
                                        missingComponent = component.GetComponentInParent(componentType);
                                    }

                                    if (missingComponent != null)
                                    {
                                        property.objectReferenceValue = missingComponent;
                                        serializedObject.ApplyModifiedProperties();
                                    }
                                    
                                    Debug.LogWarning(
                                        $"Field '{property.name}' is null on component '{component.GetType().Name}' in prefab '{prefab.name}' Prefab path {prefabPath}", prefab.gameObject);
                                }
                            }
                        }
                    }
                }
            }

            Debug.Log("Prefab field check complete.");
        }

        private static string[] GetAllPrefabPaths()
        {
            var assetPaths = AssetDatabase.GetAllAssetPaths();
            List<string> prefabPaths = new List<string>();

            foreach (var path in assetPaths)
            {
                if (path.Contains(".prefab"))
                {
                    prefabPaths.Add(path);
                }
            }

            return prefabPaths.ToArray();
        }

        private static Type GetFieldType(SerializedProperty property)
        {
            var path = property.type;
            Type type = property.serializedObject.targetObject.GetType();
            FieldInfo fieldInfo = type.GetField(property.propertyPath,
                 BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
            Type fieldType = fieldInfo?.FieldType;
            return fieldType;
        }

        private static bool IsComponentType(Type type)
        {
            return type.IsSubclassOf(typeof(Component));
        }

        private static bool IsValidReferenceType(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference: return true;
                default: return false;
            }
        }
    }
}
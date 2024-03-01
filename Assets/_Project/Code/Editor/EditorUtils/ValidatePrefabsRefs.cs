using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.EditorUtils
{
    public class ValidatePrefabFields : EditorWindow
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
                            Debug.LogWarning($"Component missing in prefab '{prefab.name}' ({prefabPath})");
                            continue;
                        }
                        
                        SerializedObject serializedObject = new SerializedObject(component);
                        SerializedProperty property = serializedObject.GetIterator();

                        while (property.NextVisible(true))
                        {
                            if (property.propertyType == SerializedPropertyType.ObjectReference &&
                                property.objectReferenceValue == null)
                            {
                                Type componentType = Type.GetType(property.propertyPath);
                                Component missingComponent;

                                if (componentType != null)
                                {
                                    if(prefab.TryGetComponent(componentType, out missingComponent))
                                    {
                                        property.objectReferenceValue = missingComponent;
                                    }
                                
                                    else if (!missingComponent)
                                    {
                                        missingComponent = prefab.GetComponentInChildren(componentType);
                                        property.objectReferenceValue = missingComponent;
                                    }

                                    else if (!missingComponent)
                                    {
                                        missingComponent = component.GetComponentInParent(componentType);
                                        property.objectReferenceValue = missingComponent;
                                    }
                                }
                                

                                serializedObject.ApplyModifiedProperties();
                                
                                
                                Debug.LogWarning($"Field '{property.name}' is null on component '{component.GetType().Name}' in prefab '{prefab.name}' ({prefabPath})");
                            }
                        }
                    }
                }
            }

            Debug.Log("Prefab field check complete.");
        }
        
        private static string[] GetAllPrefabPaths()
        {
           var assetPaths =  AssetDatabase.GetAllAssetPaths();
           List<string> prefabPaths = new List<string>();

           foreach (var path in assetPaths)
           {
               if(path.Contains(".prefab"))
               {
                   prefabPaths.Add(path);
               }
           }

           return prefabPaths.ToArray();
        }
    }
}
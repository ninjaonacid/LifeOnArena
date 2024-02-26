using System;
using System.Linq;
using Code.Runtime.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueGameObjectIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;

            if (string.IsNullOrEmpty(uniqueId.Id))
            {
                Generate(uniqueId);
            }
            else
            {
                var uniqueIds = FindObjectsOfType<UniqueId>();

                if (uniqueIds.Any(other => 
                        other != uniqueId && other.Id == uniqueId.Id)) Generate(uniqueId);
            }
        }

        private void Generate(UniqueId uniqueIdSo)
        {
            uniqueIdSo.Id = $"{uniqueIdSo.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueIdSo);
                EditorSceneManager.MarkSceneDirty(uniqueIdSo.gameObject.scene);
            }
        }
    }
}
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(UniqueID))]
    public class UniqueIDEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UniqueID uniqueId = (UniqueID)target;

            uniqueId.Name = uniqueId.name;

            uniqueId.ID = GenerateIntId(uniqueId.Name);

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
            }

        }

        private int GenerateIntId(string name)
        {
            int hash = 0;
            for (int i = 0; i < name.Length; i++)
            {
                hash = (hash << 5) - hash + name[i];
            }

            return hash;
        }
    }
}

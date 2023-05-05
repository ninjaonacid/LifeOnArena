using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(Identifier))]
    public class IdentifierEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Identifier identifier = (Identifier)target;

            identifier.Name = identifier.name;

            identifier.Id = GenerateIntId(identifier.Name);

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(identifier);
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

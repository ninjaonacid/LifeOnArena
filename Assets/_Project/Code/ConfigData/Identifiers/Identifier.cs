using UnityEngine;

namespace Code.ConfigData.Identifiers
{
    public class Identifier : ScriptableObject
    {
        [ScriptableObjectId]
        public string Name;

        [ScriptableObjectId]
        public int Id;
    }
}


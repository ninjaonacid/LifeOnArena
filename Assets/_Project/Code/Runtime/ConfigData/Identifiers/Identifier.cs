using UnityEngine;

namespace Code.Runtime.ConfigData.Identifiers
{
    public class Identifier : ScriptableObject
    {
        [ScriptableObjectId]
        public string Name;

        [ScriptableObjectId]
        public int Id;
    }
}


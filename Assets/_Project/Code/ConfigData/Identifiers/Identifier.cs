using UnityEngine;

namespace Code.ConfigData.Identifiers
{

    [CreateAssetMenu(fileName = "Identifier", menuName = "StaticData/Identifier")]
    public class Identifier : ScriptableObject
    {
        [ScriptableObjectId]
        public string Name;

        [ScriptableObjectId]
        public int Id;
    }
}


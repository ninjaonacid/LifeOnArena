using UnityEngine;

namespace Code.StaticData
{

    [CreateAssetMenu(fileName = "Identifier", menuName = "Custom/Identifier")]
    public class Identifier : ScriptableObject
    {
        [ScriptableObjectId]
        public string Name;

        [ScriptableObjectId]
        public int Id;
    }
}


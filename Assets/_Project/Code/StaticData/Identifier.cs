using UnityEngine;

namespace Code.StaticData
{

    [CreateAssetMenu(fileName = "Identifier", menuName = "Custom/Identifier")]
    public class Identifier : ScriptableObject
    {
        public string Name;
        public int Id;
    }
}

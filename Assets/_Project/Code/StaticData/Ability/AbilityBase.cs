using UnityEngine;

namespace Code.StaticData.Ability
{
    public class AbilityBase : ScriptableObject
    {
        [ScriptableObjectId]
        public string Id;
        public Sprite Icon;
    }
}

using UnityEngine;

namespace Code.Runtime.ConfigData
{
    public enum PlayLocation
    {
        Above,
        Center,
        Below
    }
    [CreateAssetMenu(fileName = "StatusVisualEffect", menuName = "AbilitySystem/StatusVisualEffects")]
    public class StatusVisualEffect : ScriptableObject
    {
        public VisualEffectData VisualEffectData;
        public PlayLocation PlayLocation;
    }
}
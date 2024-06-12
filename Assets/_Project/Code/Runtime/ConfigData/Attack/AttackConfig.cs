using Code.Runtime.ConfigData.Animations;
using UnityEngine;

namespace Code.Runtime.ConfigData.Attack
{
    [CreateAssetMenu(menuName = "Config/AttackConfig", fileName = "AttackConfig")]
    public class AttackConfig : ScriptableObject
    {
        public SlashVisualEffectConfig SlashConfig;
        public float AttackDuration;
        public SlashDirection SlashDirection;
        public float SlashDelay;
        public AnimationData AnimationData;
    }
}

using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.ConfigData.Attack
{
    [CreateAssetMenu(menuName = "Config/AttackConfig", fileName = "AttackConfig")]
    public class AttackConfig : ScriptableObject
    {
        public SlashVisualEffectConfig SlashConfig;
        public SlashDirection SlashDirection;
        public float SlashDelay;
        public AnimationData AnimationData;
        public AbilityIdentifier AttackIdentifier;
        public float ExitTime;
    }
}

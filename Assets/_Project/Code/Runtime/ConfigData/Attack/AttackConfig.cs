using Code.Runtime.ConfigData.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.ConfigData.Attack
{
    [CreateAssetMenu(menuName = "Config/AttackConfig/HeroAttackConfig", fileName = "AttackConfig")]
    public class AttackConfig : ScriptableObject
    {
        public SlashVisualEffectConfig SlashConfig;
        public SlashDirection SlashDirection;
        public float SlashDelay;
        public AnimationData AnimationData;

        [Title("Part of animation to slow")] [Range(0, 0.5f)]
        public float AnimationSpeedUpThreshold = 0.2f;
        [Title("Animation exit time")]
        public float ExitTime;
    }
}

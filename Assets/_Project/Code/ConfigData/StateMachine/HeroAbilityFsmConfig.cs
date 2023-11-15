using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [CreateAssetMenu(menuName = "Config/StateMachine", fileName = "AbilityFsmConfig")]
    public class HeroAbilityFsmConfig : ScriptableObject
    {
        public float SpinAttackDuration;
        public float DodgeRollDuration;
    }
}

using UnityEngine;

namespace Code.StaticData.Ability.PassiveAbilities
{
    [CreateAssetMenu(fileName = "AttackUp", menuName = "Ability/Passive/AttackUp")]
    public class AttackUpTemplate : PassiveAbilityTemplate<AttackUpPassive>
    {
        [Range(0.0f, 1f)] public float AttackModifier;
        public override IPassiveAbility GetAbility()
        {
            return new AttackUpPassive();
        }
    }
}

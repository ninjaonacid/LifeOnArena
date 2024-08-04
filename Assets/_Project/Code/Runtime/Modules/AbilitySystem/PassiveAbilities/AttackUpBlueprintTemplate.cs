using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.PassiveAbilities
{
    [CreateAssetMenu(fileName = "AttackUp", menuName = "AbilityTemplate/Passive/AttackUp")]
    public class AttackUpBlueprintTemplate : PassiveAbilityBlueprintTemplate<AttackUpPassive>
    {
        [Range(0.0f, 1f)] public float AttackModifier;
        public override IPassiveAbility GetAbility()
        {
            return new AttackUpPassive(AttackModifier);
        }
    }
}

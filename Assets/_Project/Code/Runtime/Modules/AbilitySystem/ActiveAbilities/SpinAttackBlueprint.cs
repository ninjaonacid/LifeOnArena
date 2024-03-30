using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "SpinAttackAbility", menuName = "AbilitySystem/SpinAttack")]
    public class SpinAttackBlueprint : ActiveAbilityBlueprint<AttackAbility>
    {
        public override ActiveAbility GetAbility()
        {
            return new AttackAbility(this);
        }
    }
}
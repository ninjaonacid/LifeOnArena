using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "SpinAttackAbility", menuName = "AbilitySystem/SpinAttack")]
    public class SpinAttackBlueprint : ActiveAbilityBlueprint<SpinAttackAbility>
    {
        public override ActiveAbility GetAbility()
        {
            return new SpinAttackAbility(this);
        }
    }
}
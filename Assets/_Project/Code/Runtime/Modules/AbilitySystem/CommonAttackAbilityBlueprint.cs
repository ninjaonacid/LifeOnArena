using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "AttackAbility", menuName = "AbilitySystem/CommonAttack")]
    public class CommonAttackAbilityBlueprint : ActiveAbilityBlueprint<AttackAbility>
    {
        public override ActiveAbility GetAbility()
        {
            return new AttackAbility(this);
        }
    }
}
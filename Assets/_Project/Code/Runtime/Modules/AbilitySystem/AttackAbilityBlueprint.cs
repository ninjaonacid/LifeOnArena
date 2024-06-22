using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "AttackAbility", menuName = "AbilitySystem/CommonAttack")]
    public class AttackAbilityBlueprint : ActiveAbilityBlueprint<AttackAbility>
    {
        public override ActiveAbility GetAbility()
        {
            return new AttackAbility(this);
        }
    }
}
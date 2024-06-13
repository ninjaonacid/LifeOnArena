using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "CommonAttackAbility", menuName = "AbilitySystem/CommonAttack")]
    public class CommonAttackAbilityBlueprint : ActiveAbilityBlueprint<CommonAttackAbility>
    {
        public override ActiveAbility GetAbility()
        {
            return new CommonAttackAbility(this);
        }
    }
}
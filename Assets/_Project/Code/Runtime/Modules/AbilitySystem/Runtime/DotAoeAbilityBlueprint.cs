using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "DotAoeAbility", menuName = "AbilitySystem/ActiveAbility/DotAoeAbility")]
    public class DotAoeAbilityBlueprint : ActiveAbilityBlueprintBase
    {
        [SerializeField] private float _castDistance;
        public override ActiveAbility GetAbility()
        {
            return new DotAoeAbility(this, _castDistance);
        }
    }
}
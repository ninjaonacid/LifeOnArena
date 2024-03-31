using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "AoeAbility", menuName = "AbilitySystem/ActiveAbility/AoeAbility")]
    public class AoeAbilityBlueprint : ActiveAbilityBlueprintBase
    {
        [SerializeField] private float _castDistance;
        [SerializeField] private float _duration;
        [SerializeField] private float _aoeRadius;
        public override ActiveAbility GetAbility()
        {
            return new AoeAbility(this, _castDistance, _duration, _aoeRadius);
        }
    }
}
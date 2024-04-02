using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "AoeAbility", menuName = "AbilitySystem/ActiveAbility/AoeAbility")]
    public class AoeAbilityBlueprint : ActiveAbilityBlueprintBase
    {
        [SerializeField] protected float _castDistance;
        [SerializeField] protected float _duration;
        [SerializeField] protected float _aoeRadius;
        public override ActiveAbility GetAbility()
        {
            return new AoeAbility(this, _castDistance, _duration, _aoeRadius);
        }
    }
}
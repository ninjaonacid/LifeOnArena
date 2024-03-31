using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "FireWall", menuName = "AbilitySystem/Ability/FireWall")]
    public class FireWallBlueprint : ActiveAbilityBlueprint<AoeAbility>
    {
        [SerializeField] private float _castDistance;
        [SerializeField] private float _duration;
        [SerializeField] private float _aoeRadius;
        
        public override ActiveAbility GetAbility()
        {
            return new DotAoeAbility(this, _castDistance, _duration, _aoeRadius);
        }
    }
    
}

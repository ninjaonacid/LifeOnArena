using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Tornado", menuName = "AbilitySystem/Ability/Tornado")]
    public class TornadoBlueprint : ActiveAbilityBlueprint<AoeAbility>
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _castDistance;
        [SerializeField] private float _lifeTime;
        
        public override ActiveAbility GetAbility()
        {
            return new AoeAbility
                (this,
                _lifeTime,
                _radius,
                _castDistance);
        }
        
    }
}

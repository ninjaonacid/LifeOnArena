using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Tornado", menuName = "AbilitySystem/Ability/Tornado")]
    public class TornadoBlueprint : ActiveAbilityBlueprint<TornadoAbility>
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _castDistance;
        [SerializeField] private float _lifeTime;
        
        public override ActiveAbility GetAbility()
        {
            return new TornadoAbility
                (_visualEffectFactory,
                _battleService,
                VisualEffectData,
                StatusEffects,
                _lifeTime,
                _radius,
                _castDistance);
        }
        
    }
}

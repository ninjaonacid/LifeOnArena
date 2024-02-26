using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "TornadoAbility", menuName = "Config/AbilityData/Cast/TornadoAbility")]
    public class TornadoTemplate : AbilityTemplate<TornadoAbility>
    {
        public float Damage;
        public float Radius;
        public float CastDistance;
        public float ProjectileLifetime;

        private IAbility _abilityInstance;
        public override IAbility GetAbility()
        {
            return _abilityInstance ??= 
                new TornadoAbility
                (VisualEffectFactory,
                _battleService,
                VisualEffectData,
                StatusEffects,
                ProjectileLifetime,
                Damage,
                Radius,
                CastDistance);
        }
        
    }
}

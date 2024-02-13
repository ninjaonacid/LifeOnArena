using System.Collections.Generic;
using Code.ConfigData.StatusEffect;
using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "TornadoAbility", menuName = "Config/AbilityData/Cast/TornadoAbility")]
    public class TornadoTemplate : AbilityTemplate<TornadoAbility>
    {
        public float Damage;
        public float AttackRadius;
        public float CastDistance;
        public float ProjectileLifetime;

        public List<StatusEffectTemplateBase> StatusEffects;

        private IAbility _abilityInstance;
        public override IAbility GetAbility()
        {
            return _abilityInstance ??= 
                new TornadoAbility
                (_particleFactory,
                _battleService,
                VfxData,
                ProjectileLifetime,
                Damage,
                AttackRadius,
                CastDistance);
        }
    }
}

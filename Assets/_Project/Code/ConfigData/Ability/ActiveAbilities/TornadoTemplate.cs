using System.Collections.Generic;
using System.Linq;
using Code.ConfigData.StatusEffects;
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

        [SerializeField] private List<StatusEffectTemplateBase> _statusEffects;

        private IReadOnlyList<StatusEffect> StatusEffects => _statusEffects.Select(x => x.GetStatusEffect()).ToList();

        private IAbility _abilityInstance;
        public override IAbility GetAbility()
        {
            return _abilityInstance ??= 
                new TornadoAbility
                (_particleFactory,
                _battleService,
                VfxData,
                StatusEffects,
                ProjectileLifetime,
                Damage,
                AttackRadius,
                CastDistance);
        }
        
    }
}

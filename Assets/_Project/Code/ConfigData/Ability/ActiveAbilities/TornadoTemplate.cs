using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "TornadoAbility", menuName = "Config/AbilityData/Cast/TornadoAbility")]
    public class TornadoTemplate : AbilityTemplate<TornadoAbility>
    {
        public float Damage;
        public float AttackRadius;
        public float CastDistance;

        private IAbility _abilityInstance;
        public override IAbility GetAbility()
        {
            return _abilityInstance ??= 
                new TornadoAbility
                (ParticleObjectPool,
                BattleService,
                ParticleObjectData,
                ActiveTime,
                Damage,
                AttackRadius,
                CastDistance);
        }
    }
}

using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Tornado", menuName = "AbilityData/Cast/Tornado")]
    public class TornadoTemplate : AbilityTemplate<Tornado>
    {
        public AssetReference TornadoVfx;
        public float Damage;
        public float AttackRadius;
        public float CastDistance;

        private IAbility _abilityInstance;
        public override IAbility GetAbility()
        {
            return _abilityInstance ??= 
                new Tornado
                (ParticlePool,
                BattleService,
                TornadoVfx,
                ActiveTime,
                Damage,
                AttackRadius);
        }
    }
}

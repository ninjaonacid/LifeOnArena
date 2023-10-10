using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "TornadoAbility", menuName = "AbilityData/Cast/TornadoAbility")]
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
                (ViewPool,
                BattleService,
                ViewObjectData,
                ActiveTime,
                Damage,
                AttackRadius,
                CastDistance);
        }
    }
}

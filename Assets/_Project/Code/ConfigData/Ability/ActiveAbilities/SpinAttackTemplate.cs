using Code.Entity.EntitiesComponents;
using Code.Logic.EntitiesComponents;
using Code.Services.BattleService;
using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "SpinAttackAbility", menuName = "AbilityData/Attack/SpinAttack")]
    public class SpinAttackTemplate : AbilityTemplate<SpinAttack>
    {
        public float Damage;
        public override IAbility GetAbility()
        {
            return new SpinAttack(Damage, _battleService);
        }
    }

    public class SpinAttack : IAbility
    {
        private float _damage;
        private readonly IBattleService _battleService;
        private readonly LayerMask _layerMask = 1 << LayerMask.NameToLayer("Hittable");

        public SpinAttack(float damage, IBattleService battleService)
        {
            _damage = damage;
            _battleService = battleService;
        }


        public void Use(GameObject caster, GameObject target)
        {
            var attack = caster.GetComponent<IAttack>();
            
            
        }
    }
}

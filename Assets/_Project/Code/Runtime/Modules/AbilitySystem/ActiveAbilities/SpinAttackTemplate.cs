using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Services.BattleService;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
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
        private readonly BattleService _battleService;
        private readonly LayerMask _layerMask = 1 << LayerMask.NameToLayer("Hittable");

        public SpinAttack(float damage, BattleService battleService)
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

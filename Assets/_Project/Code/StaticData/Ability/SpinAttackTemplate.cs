using Code.Hero.HeroStates;
using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(fileName = "SpinAttackAbility", menuName = "AbilityData/Attack/SpinAttack")]
    public class SpinAttackTemplate : AbilityTemplate<SpinAttack>
    {
        public float Damage;
        public override IAbility GetAbility()
        {
            return new SpinAttack(Damage);
        }
    }

    public class SpinAttack : IAbility
    {
        private float _damage;


        public SpinAttack(float damage)
        {
            _damage = damage;
        }

        public void Use(GameObject target, GameObject caster)
        {
           
        }

    }
}

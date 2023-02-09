using Code.Data;
using Code.Logic;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, IAttack, ISaveReader
    {
        private static int _layerMask;
        private readonly Collider[] _hits = new Collider[2];
        private CharacterStats _characterStats;
        private AbilityTemplateBase _activeSkill;

        public HeroWeapon HeroWeapon;
        public CharacterController CharacterController;
        public HeroAnimator HeroAnimator;

        public void LoadProgress(PlayerProgress progress)
        {
            _characterStats = progress.CharacterStats;
        }

        private void Awake()
        {

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        public void SetActiveSkill(AbilityTemplateBase attackSkill)
        {
            _activeSkill = attackSkill;
        }

        public void BaseAttack()
        {
            if (HeroWeapon.GetEquippedWeapon() != null)
            {
                DoDamage(_characterStats.CalculateHeroDamage() +
                         HeroWeapon.GetEquippedWeapon().Damage,
                    HeroWeapon.GetEquippedWeapon().AttackRadius);
            } 
            else DoDamage(_characterStats.CalculateHeroDamage(), _characterStats.CalculateHeroAttackRadius());
        }

        public void SkillAttack()
        {
            if (HeroWeapon.GetEquippedWeapon() != null)
            {
                DoDamage(//_activeSkill.Damage +
                         HeroWeapon.GetEquippedWeapon().Damage,
                    HeroWeapon.GetEquippedWeapon().AttackRadius);
            }
            else DoDamage(_characterStats.CalculateHeroDamage(), _characterStats.CalculateHeroAttackRadius());
        }

        public void DoDamage(float damage, float attackRadius)
        {
            for (var i = 0; i < Hit(attackRadius); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(damage);

                Debug.Log("BaseDamage amount: " + _characterStats.CalculateHeroDamage());
                Debug.Log(_hits[i].ToString());
            }
        }

        private int Hit(float attackRadius)
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward,
                _characterStats.CalculateHeroAttackRadius(),
                _hits,
                _layerMask);
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, CharacterController.center.y, transform.position.z + 1f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StartPoint() + transform.forward, _characterStats.CalculateHeroAttackRadius());
        }
    }
}
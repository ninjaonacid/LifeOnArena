using Code.Data;
using Code.Hero.HeroStates;
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
        private AttackDefinition _activeSkill;


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

        public void SetActiveSkill(AttackDefinition attackSkill)
        {
            _activeSkill = attackSkill;
        }

        public void BaseAttack()
        {
            DoDamage(_characterStats.BaseDamage);
        }

        public void SkillAttack()
        {
            DoDamage(_activeSkill.Damage);
        }

        public void DoDamage(float damage)
        {
            for (var i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(damage);

                Debug.Log("BaseDamage amount: " + _characterStats.BaseDamage);
                Debug.Log(_hits[i].ToString());
            }
        }

       /* private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StartPoint() + transform.forward, _characterStats.BaseAttackRadius);
        }*/

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward,
                _characterStats.BaseAttackRadius ,
                _hits,
                _layerMask);
        }


        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, CharacterController.center.y, transform.position.z + 1f);
        }
    }
}
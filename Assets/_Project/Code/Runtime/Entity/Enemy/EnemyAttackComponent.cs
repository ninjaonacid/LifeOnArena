using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyAttackComponent : EntityAttackComponent
    {
        [SerializeField] private float _attackInterval;
        public bool TargetInMeleeAttackRange => _meleeAttackIsActive;
        
        private float _attackCooldown;
        
        private bool _meleeAttackIsActive;
        private bool _isAttacking;
        
        private void OnDisable()
        {
            _meleeAttackIsActive = false;
        }

        private void OnEnable()
        {
            _isAttacking = false;
            _meleeAttackIsActive = false;
        }

        private void Update()
        {
            UpdateCooldown();
        }

        public void EnableAttack()
        {
            _meleeAttackIsActive = true;
        }
        
        public void DisableAttack()
        {
            _meleeAttackIsActive = false;
        }
        
        private void UpdateCooldown()
        {
            if (!CoolDownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        public void AttackEnded()
        {
            _attackCooldown = _attackInterval;
            _isAttacking = false;
        }

        public virtual bool CanAttack()
        {
            return CoolDownIsUp() && !_isAttacking && _meleeAttackIsActive;
        }
        
        private bool CoolDownIsUp()
        {
            return _attackCooldown <= 0f;
        }
    }
}
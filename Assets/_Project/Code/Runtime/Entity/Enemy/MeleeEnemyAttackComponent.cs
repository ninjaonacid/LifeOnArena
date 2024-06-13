using Code.Runtime.Services.BattleService;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class MeleeEnemyAttackComponent : EntityAttackComponent
    {
        private float _attackCooldown;
        private bool _meleeAttackIsActive;
        public bool TargetInMeleeAttackRange => _meleeAttackIsActive;
        
        private bool _isAttacking;
        
        private BattleService _battleService;
        
        [Inject]
        public void Construct(BattleService battleService)
        {
            _battleService = battleService;
        }

        private void OnDisable()
        {
            _meleeAttackIsActive = false;
        }

        private void OnEnable()
        {
            _isAttacking = false;
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
            _attackCooldown = GetAttacksPerSecond();
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
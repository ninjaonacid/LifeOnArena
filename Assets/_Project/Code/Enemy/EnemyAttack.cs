using System;
using System.Linq;
using Code.Logic;
using Code.Logic.EntitiesComponents;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {

        [SerializeField] private EnemyConfig _config;
        private float _attackCooldown;
        private bool _attackIsActive;

        public bool TargetInAttackRange => _attackIsActive;
        private readonly Collider[] _hits = new Collider[1];

        private bool _isAttacking;

        private int _layerMask;


        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("PlayerHitBox");
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
            _attackIsActive = true;
        }


        public void DisableAttack()
        {
            _attackIsActive = false;
        }
        

        private void UpdateCooldown()
        {
            if (!CoolDownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        public void AttackEnded()
        {
            _attackCooldown = _config.AttackCooldown;
            _isAttacking = false;
        }

        public bool CanAttack()
        {
            return CoolDownIsUp() && !_isAttacking && _attackIsActive;
        }

        public void Attack()
        {
            _isAttacking = true;
            if (Hit(out var hit)) hit.transform.GetComponentInParent<IHealth>().TakeDamage(_config.Damage);
        }

        private bool Hit(out Collider hit)
        {
            var startPoint = StartPoint();

            var hitscount = Physics.OverlapSphereNonAlloc(startPoint,
                _config.Cleavage,
                _hits,
                _layerMask);
            hit = _hits.FirstOrDefault();
            return hitscount > 0;
        }


        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x,
                transform.position.y + 2f,
                transform.position.z) + transform.forward * _config.EffectiveDistance;
        }

        private bool CoolDownIsUp()
        {
            return _attackCooldown <= 0f;
        }
    }
}
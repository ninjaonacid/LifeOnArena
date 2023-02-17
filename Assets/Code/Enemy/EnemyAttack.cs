using System;
using System.Linq;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        private float _attackCooldown;
        private bool _attackIsActive;

        public bool TargetInAttackRange => _attackIsActive;
        private Transform _heroTransform;
        private readonly Collider[] _hits = new Collider[1];

        private bool _isAttacking;

        private int _layerMask;
  

        public float AttackCooldown;
        public float AttackDuration;
        public float Cleavage;
        public float Damage;
        public float EffectiveDistance;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
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

        public void LookAtTarget()
        {
            transform.LookAt(_heroTransform);
        }

        private void UpdateCooldown()
        {
            if (!CoolDownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        public bool CanAttack()
        {
            return CoolDownIsUp() && !_isAttacking && _attackIsActive;
        }

        private void OnAttack()
        {
            _isAttacking = true;
            if (Hit(out var hit)) hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
        }

        private bool Hit(out Collider hit)
        {
            var startPoint = StartPoint();

            var hitscount = Physics.OverlapSphereNonAlloc(startPoint,
                Cleavage,
                _hits,
                _layerMask);
            hit = _hits.FirstOrDefault();
            return hitscount > 0;
        }


        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x,
                transform.position.y + 2f,
                transform.position.z) + transform.forward * EffectiveDistance;
        }

        private void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        private bool CoolDownIsUp()
        {
            return _attackCooldown <= 0f;
        }
    }
}
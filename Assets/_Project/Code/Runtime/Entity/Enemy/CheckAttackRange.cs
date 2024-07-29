using System;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAttackComponent))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttackComponent _attackComponent;
        [SerializeField] private EnemyTarget _enemyTarget;
        [SerializeField] private StatController _statController;

        [SerializeField] private bool _showGizmos;
        [SerializeField] private Color _gizmoColor = Color.yellow;

        private bool _isInRadius;
        private Transform _target => _enemyTarget.GetTargetTransform();
        private int _attackDistance;
        private int _checkRadius;

        private void Start()
        {
            if (_statController.IsInitialized)
            {
                _attackDistance = _statController.Stats["AttackDistance"].Value;
            }
            else
            {
                _statController.Initialized += () => _attackDistance = _statController.Stats["AttackDistance"].Value;
            }
        }

        private void Update()
        {
            if (_enemyTarget.HasTarget())
            {
                CheckDistance();
            }
            else
            {
                AttackRangeExit();
            }
        }

        protected virtual void AttackRangeExit()
        {
            _attackComponent.DisableAttack();
        }

        protected virtual void AttackRangeEnter()
        {
            _attackComponent.EnableAttack();
        }

        private void CheckDistance()
        {
            float sqrDistance = (transform.position - _target.position).sqrMagnitude;
            float sqrRadius = _attackDistance * _attackDistance;

            _isInRadius = sqrDistance <= sqrRadius;

            if (_isInRadius)
            {
                AttackRangeEnter();
            }
            else
            {
                AttackRangeExit();
            }
        }

        void OnDrawGizmosSelected()
        {
            if (!_showGizmos) return;

            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);

            if (_target != null)
            {
                Gizmos.DrawLine(transform.position, _target.position);

                float distance = Vector3.Distance(transform.position, _target.position);
                Vector3 midPoint = (transform.position + _target.position) / 2f;

#if UNITY_EDITOR
                UnityEditor.Handles.Label(midPoint, $"Distance: {distance:F2}");
#endif
            }
        }
    }
}
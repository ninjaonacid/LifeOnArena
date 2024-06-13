using System;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAttackComponent))]
    public class CheckMeleeAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttackComponent _attackComponent;
        [SerializeField] private TriggerObserver _meleeRangeObserver;

        private void OnEnable()
        {
            _meleeRangeObserver.TriggerEnter += MeleeRangeEnter;
            _meleeRangeObserver.TriggerExit += MeleeRangeExit;

            _attackComponent.DisableAttack();
        }

        private void OnDisable()
        {
            _meleeRangeObserver.TriggerEnter -= MeleeRangeEnter;
            _meleeRangeObserver.TriggerExit -= MeleeRangeExit;
        }

        private void MeleeRangeExit(Collider obj)
        {
            _attackComponent.DisableAttack();
        }

        private void MeleeRangeEnter(Collider obj)
        {
            _attackComponent.EnableAttack();
        }
    }
}
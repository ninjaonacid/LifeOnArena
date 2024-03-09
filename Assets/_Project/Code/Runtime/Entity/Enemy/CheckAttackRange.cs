using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAttackComponent))]
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttackComponent AttackComponent;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            AttackComponent.DisableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            AttackComponent.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            AttackComponent.EnableAttack();
        }
    }
}
using UnityEngine;

namespace Code.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttack Attack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            Attack.DisableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            Attack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            Attack.EnableAttack();
        }
    }
}
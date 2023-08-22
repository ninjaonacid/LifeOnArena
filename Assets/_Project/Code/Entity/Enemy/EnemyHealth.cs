using UnityEngine;

namespace Code.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : EntityHealth
    {
        public EnemyAnimator Animator;
        
        private void OnDisable()
        {
            Health.ResetHealth();
        }
        
    }
}
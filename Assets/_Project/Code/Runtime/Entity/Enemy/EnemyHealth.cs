using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyHealth : EntityHealth
    {
        private void OnDisable()
        {
            Health.ResetHealth();
        }
    }
}
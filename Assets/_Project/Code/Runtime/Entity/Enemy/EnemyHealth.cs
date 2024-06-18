using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyHealth : EntityHealth
    {
        [SerializeField] private bool _isNeedResetAfterDisable = true;
        private void OnDisable()
        {
            if(_isNeedResetAfterDisable)
                Health.ResetHealth();
        }
    }
}
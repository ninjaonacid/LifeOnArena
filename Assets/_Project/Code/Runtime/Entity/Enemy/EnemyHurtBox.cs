using System.Threading;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyHurtBox : EntityHurtBox
    {
        private float _hitBoxTimer;
        private CancellationTokenSource _cts;
        
        [SerializeField] private TriggerObserver _observer;
        
    }
}

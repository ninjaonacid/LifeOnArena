using System;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyStaggerComponent : MonoBehaviour
    {
        public event Action Staggered;
        
        [SerializeField] private StatController _stats;
        [SerializeField] private float _staggerInterval;

        private float _staggerDuration;
        private ITimer _staggerTimer;
        private bool _isStaggered = false;

        private bool IsCanBeStaggered => 
            _staggerDuration != 0 && 
            !_isStaggered && 
            _staggerInterval < _staggerTimer.Elapsed;

        public void Initialize()
        {
            _staggerTimer = new Timer();
            
            if (_stats.IsInitialized)
            {
                InitializeStaggerDuration();
            }
            else
            {
                _stats.Initialized += InitializeStaggerDuration;
            }
        }

        private void Update()
        {
            if(_isStaggered)
            {
                if (_staggerTimer.Elapsed >= _staggerDuration)
                {
                    _isStaggered = false;
                }
            }
        }

        private void InitializeStaggerDuration()
        {
            _staggerDuration = _stats.Stats["HitRecovery"].Value;
            var health = (Health)_stats.Stats["Health"];
            health.CurrentValueChanged += OnDamage;
        }

        private void OnDamage()
        {
            if (IsCanBeStaggered)
            {
                TriggerStagger();
            }
        }

        private void TriggerStagger()
        {
            _isStaggered = true;
            _staggerTimer.Reset();
            Staggered?.Invoke();
        }
    }
}

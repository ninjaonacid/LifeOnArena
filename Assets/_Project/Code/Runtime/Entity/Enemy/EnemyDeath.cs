using System;
using System.Collections;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private StatController _stats;
        public EnemyAnimator Animator;
        public EnemyHealth Health;
        public GameObject FracturedPrefab;
        public GameObject EnemyModel;

        public event Action Happened;

        private void OnEnable()
        {
            if (_stats.IsInitialized)
            {
                Health.Health.CurrentValueChanged += HealthChanged;
            }
            else
            {
                _stats.Initialized += () => ((Health)_stats.Stats["Health"]).CurrentValueChanged += HealthChanged;
            }
        }

        private void OnDestroy()
        {
            Health.Health.CurrentValueChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (Health.Health.CurrentValue <= 0) Die();
        }

        private void Die()
        {
            Health.Health.CurrentValueChanged -= HealthChanged;
            Animator.PlayDeath();
            //EnemyModel.SetActive(false);
           // FracturedPrefab.SetActive(true);
            //FracturedPrefab.GetComponentInChildren<Rigidbody>().AddExplosionForce(10f, Vector3.down, 1f);
            StartCoroutine(DestroyTimer());
            
            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(1);
           // EnemyModel.SetActive(true);
           // FracturedPrefab.SetActive(false);
            gameObject.SetActive(false);
        }

        
    }
}
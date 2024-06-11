using System;
using System.Collections;
using Code.Runtime.Modules.StatSystem;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(StatController))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private StatController _stats;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private GameObject _fracturedPrefab;
        [SerializeField] private GameObject _enemyModel;
        [SerializeField] private EnemyHurtBox _enemyHurtBox;
        
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxHeight;

        public bool IsDead { get; private set; }

        public event Action Happened;

        private void OnEnable()
        {
            if (_stats.IsInitialized)
            {
                _health.Health.CurrentValueChanged += HealthChanged;
            }
            else
            {
                _stats.Initialized += () => ((Health)_stats.Stats["Health"]).CurrentValueChanged += HealthChanged;
            }

            IsDead = false;
        }

        private void OnDestroy()
        {
            _health.Health.CurrentValueChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.Health.CurrentValue <= 0) Die();
        }

        private void Die()
        {
            _health.Health.CurrentValueChanged -= HealthChanged;
            _enemyModel.SetActive(false);
            

            if (_fracturedPrefab is not null)
            {
                _fracturedPrefab.SetActive(true);

                foreach (Rigidbody rb in _fracturedPrefab.GetComponentsInChildren<Rigidbody>())
                {
                    Vector3 force = _enemyHurtBox.LastHitDirection * Random.Range(1f, 10f);
                    force.y = Mathf.Clamp(force.y, -2f, 3f);
                    rb.AddForce(force, ForceMode.Impulse);
                    rb.AddTorque(new Vector3(0, 1f, 0));
                }
            }
            
            _enemyHurtBox.DisableCollider(true);

            StartCoroutine(DestroyTimer());

            IsDead = true;
            
            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(5);
            _enemyModel.SetActive(true);
            _fracturedPrefab.SetActive(false);
            gameObject.SetActive(false);
        }

        
    }
}
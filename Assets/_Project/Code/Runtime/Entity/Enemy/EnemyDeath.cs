using System;
using System.Collections;
using System.Collections.Generic;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.UI.View.HUD;
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
        [SerializeField] private EntityUI _entityUI;
        [SerializeField] private GameObject _fracturedPrefab;
        [SerializeField] private GameObject _enemyModel;
        [SerializeField] private EnemyHurtBox _enemyHurtBox;
        
        
        [SerializeField] private bool _isAnimatedDeath;
        [SerializeField] private float _disappearDuration = 4f;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxHeight;

        private Rigidbody[] _fractureParts;

        public bool IsDead { get; private set; }

        public event Action Happened;

        private void Awake()
        {
            if (!_isAnimatedDeath)
            {
                _fractureParts = _fracturedPrefab.GetComponentsInChildren<Rigidbody>();
            }
        }

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
            _entityUI.SetActiveHpView(true);
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
            
            if (!_isAnimatedDeath)
            {
                _enemyModel.SetActive(false);
                _entityUI.SetActiveHpView(false);

                if (_fracturedPrefab is not null)
                {
                    _fracturedPrefab.SetActive(true);

                    foreach (Rigidbody rb in _fractureParts)
                    {
                        Vector3 force = (transform.position - _enemyHurtBox.LastHitPosition).normalized *
                                        Random.Range(10f, 20f);
                        force.y = Mathf.Clamp(force.y, -2f, 3f);
                        rb.AddForce(force, ForceMode.Impulse);
                        rb.AddTorque(new Vector3(0, 1f, 0));
                    }
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

            ResetObject();
        }


        private void ResetObject()
        {
            if (!_isAnimatedDeath)
            {
                _fracturedPrefab.SetActive(false);
            }
            gameObject.SetActive(false);
            
            if(_enemyModel != null)
                _enemyModel.SetActive(true);
        }
    }
}
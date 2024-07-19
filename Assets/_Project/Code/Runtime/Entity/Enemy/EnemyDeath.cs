using System;
using System.Collections;
using System.Collections.Generic;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.UI.View.HUD;
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
        [SerializeField] private EnemyTarget _enemyTarget;
        [SerializeField] private PooledObject _poolable;
        
        [SerializeField] private bool _isAnimatedDeath;
        [SerializeField] private float _disappearDuration = 4f;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxHeight;

        private Rigidbody[] _fractureParts;
        private List<Vector3> _positions = new();
        private List<Quaternion> _rotations = new();

        public bool IsDead { get; private set; }

        public event Action Happened;

        public void Initialize()
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

        private void Awake()
        {
            if (!_isAnimatedDeath)
            {
                _fractureParts = _fracturedPrefab.GetComponentsInChildren<Rigidbody>();

                foreach (var part in _fractureParts)
                {
                    _positions.Add(part.transform.localPosition);
                    _rotations.Add(part.transform.localRotation);
                }
            }
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
            
            HandleDeathVisuals();
            DisableEnemyInteractions();

            StartCoroutine(ReturnToPoolTimer());


            Happened?.Invoke();
        }

        private void DisableEnemyInteractions()
        {
            _enemyHurtBox.DisableCollider(true);
            IsDead = true;
        }

        private void HandleDeathVisuals()
        {
            if (!_isAnimatedDeath)
            {
                _enemyModel.SetActive(false);
                _entityUI.SetActiveHpView(false);

                if (_fracturedPrefab is not null)
                {
                    _fracturedPrefab.SetActive(true);

                    foreach (Rigidbody rb in _fractureParts)
                    {
                        Vector3 force = (transform.position - _enemyTarget.GetTargetTransform().position).normalized *
                                        Random.Range(_minForce, _maxForce);
                        force.y = Mathf.Clamp(force.y, -2f, _maxHeight);
                        rb.AddForce(force, ForceMode.Impulse);
                        rb.AddTorque(new Vector3(0, 1f, 0));
                    }
                }
            }
        }

        private IEnumerator ReturnToPoolTimer()
        {
            yield return new WaitForSeconds(_disappearDuration);

            ResetObject();
            
            _poolable.ReturnToPool();
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
            
            _enemyHurtBox.DisableCollider(false);

            for (var index = 0; index < _fractureParts.Length; index++)
            {
                var part = _fractureParts[index];
                part.transform.localRotation = _rotations[index];
                part.transform.localPosition = _positions[index];
            }
        }
    }
}
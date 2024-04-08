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
        [SerializeField] private float _distance;
        [SerializeField] private float _maxHeight;
        [SerializeField] private float _flightDuration;

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

                foreach (Transform obj in _fracturedPrefab.transform)
                {
                    Vector2 randomPoint = Random.insideUnitCircle.normalized;
                    Vector3 direction2D = new Vector3(randomPoint.x, 0, randomPoint.y);

                    var forward = transform.forward;
                    float angle = Vector3.SignedAngle(Vector3.forward, direction2D, -forward);
                    angle = Mathf.Clamp(angle, Random.Range(-45f, 0), Random.Range(0, 45f));

                    Quaternion rotation = Quaternion.AngleAxis(angle, -forward);
                    
                    Vector3 direction = rotation * -forward;

                    var position = obj.position;
                    Vector3 targetPosition = position + direction * _distance;

                    Vector3 startPoint = position;
                    Vector3 endPoint = targetPosition;
                    Vector3 controlPoint = (startPoint + endPoint) / 2f + Vector3.up * _maxHeight;
                    
                    obj.DOMove(endPoint, _flightDuration).SetEase(Ease.OutQuad);

                    obj.DOLocalMoveY(0, _flightDuration).SetEase(Ease.OutQuad);

                }
            }

            StartCoroutine(DestroyTimer());

            IsDead = true;
            
            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(2);
            _enemyModel.SetActive(true);
            _fracturedPrefab.SetActive(false);
            gameObject.SetActive(false);
        }

        
    }
}
using System;
using System.Collections;
using Code.Runtime.Modules.StatSystem;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public bool IsDead { get; private set; }

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

            IsDead = false;
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
            EnemyModel.SetActive(false);
            FracturedPrefab.SetActive(true);
            
            foreach (Transform obj in FracturedPrefab.transform)
            {
                Vector2 randomPoint = Random.insideUnitCircle.normalized;
                
                //float angle = Vector3.SignedAngle()

                Vector3 direction2D = new Vector3(randomPoint.x, 0, randomPoint.y);
                Vector3 direction3D = transform.TransformDirection(direction2D).normalized;
                float explosionForce = Random.Range(2f, 5f);
                float distance = Random.Range(0f, 10f);
                float randomAngle = Random.Range(-90 / 2f, 90 / 2f);
                float maxHeight = 2f;
                float flightDuration = 2f;
                
                float angle = Vector3.SignedAngle(Vector3.forward, direction2D, -transform.forward);
                angle = Mathf.Clamp(angle, Random.Range(-45f, 0), Random.Range(0, 45f));

                Quaternion rotation = Quaternion.AngleAxis(angle, -transform.forward);


                Vector3 direction = rotation * -transform.forward;

                Vector3 targetPosition = obj.position + direction * distance;

                Vector3 startPoint = obj.position;
                Vector3 endPoint = targetPosition;
                Vector3 controlPoint = (startPoint + endPoint) / 2f + Vector3.up * maxHeight;

                
                obj.DOMove(endPoint, flightDuration).SetEase(Ease.OutQuad);
                
                obj.DOLocalMoveY(0, flightDuration).SetEase(Ease.OutQuad);
                
            }
            
            StartCoroutine(DestroyTimer());

            IsDead = true;
            
            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(2);
            EnemyModel.SetActive(true);
            FracturedPrefab.SetActive(false);
            gameObject.SetActive(false);
        }

        
    }
}
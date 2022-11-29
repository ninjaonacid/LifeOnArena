using System;
using System.Collections;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyAnimator Animator;
        public EnemyHealth Health;
        public GameObject FracturedPrefab;
        public GameObject EnemyModel;

        public event Action Happened;

        public void OnEnable()
        {
            Health.HealthChanged += HealthChanged;
        }

        private void OnDisable()
        {
            Health.HealthChanged -= HealthChanged;
        }


        private void HealthChanged()
        {
            if (Health.Current <= 0) Die();
        }

        private void Die()
        {
            Health.HealthChanged -= HealthChanged;
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
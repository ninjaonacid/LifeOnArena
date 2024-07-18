using System;
using Code.Runtime.Logic.Animator;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Dying");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

        private Animator _enemyAnimator;

        public AnimatorState State { get; private set; }

        public void PlayAnimation(int hash)
        {
            _enemyAnimator.CrossFade(hash, 0.1f);
            
        }

        private void Awake()
        {
            _enemyAnimator = GetComponent<Animator>();
        }

        public void PlayAnimation(int hash, string parameter, float value)
        {
            _enemyAnimator.CrossFade(hash, 0.1f);
        }

        public float GetFloat(string parameterName)
        {
            return _enemyAnimator.GetFloat(parameterName);
        }

        public void SetAttackAnimationSpeed(float value)
        {
            _enemyAnimator.SetFloat(AttackSpeed,  value);
        }

        public void PlayDie() => _enemyAnimator.Play(Die);
    }
}
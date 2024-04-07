using System;
using Code.Runtime.Logic.Animator;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int GetHit = Animator.StringToHash("GetHit");

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Attack = Animator.StringToHash("Attack01");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");

        private Animator _enemyAnimator;

        public AnimatorState State { get; private set; }

        public void PlayAnimation(int hash)
        {
            _enemyAnimator.CrossFade(hash, 0.1f);
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

        public void EnteredState(int stateHash)
        
        {
            throw new NotImplementedException();
        }

        public void ExitedState(int stateHash)
        {
            throw new NotImplementedException();
        }

        private void Awake()
        {
            _enemyAnimator = GetComponent<Animator>();
        }
    }
}
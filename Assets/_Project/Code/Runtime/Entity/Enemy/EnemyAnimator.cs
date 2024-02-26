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

        private Animator _enemyAnimator;

        public AnimatorState State { get; private set; }

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

        public void PlayDeath()
        {
            _enemyAnimator.SetTrigger(Die);
        }

        public void PlayHit()
        {
            _enemyAnimator.CrossFade(GetHit, 0.1f);
        }

        public void PlayIdle()
        {
            _enemyAnimator.CrossFade(Idle, 0.5f);
        }
        public void Move()
        {
            _enemyAnimator.CrossFade(Run, 0.1f);
            
        }

        public void StopMoving()
        {
            _enemyAnimator.SetBool(IsMoving, false);
        }

        public void PlayAttack()
        {
            _enemyAnimator.CrossFade(Attack, 0.5f);
        }
    }
}
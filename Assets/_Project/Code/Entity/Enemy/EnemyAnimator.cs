using System;
using Code.Logic.Animator;
using UnityEngine;

namespace Code.Entity.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int GetHit = Animator.StringToHash("GetHit");
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
            _enemyAnimator.SetTrigger(GetHit);
        }

        public void PlayIdle()
        {
            _enemyAnimator.CrossFade(Idle, 0.5f);
        }
        public void Move(float speed)
        {
            _enemyAnimator.SetFloat(Speed, speed);
            _enemyAnimator.SetBool(IsMoving, true);
        }

        public void StopMoving()
        {
            _enemyAnimator.SetBool(IsMoving, false);
        }

        public void PlayAttack()
        {
            _enemyAnimator.SetTrigger(Attack);
        }
    }
}
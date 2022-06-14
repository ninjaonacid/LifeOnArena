using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Move = Animator.StringToHash("Move");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _dieStateHash = Animator.StringToHash("Dying");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _runStateHash = Animator.StringToHash("Run");
        private CharacterController _characterController;


        private Animator _heroAnimator;

        public AnimatorState State { get; private set; }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(StateFor(stateHash));
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        private void Awake()
        {
            _heroAnimator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Moving();
        }

        private void Moving()
        {
            _heroAnimator.SetFloat(Move,
                _characterController.velocity.magnitude,
                0.1f,
                Time.deltaTime);
        }

        public bool IsAttacking()
        {
            return State == AnimatorState.Attack;
        }

        public void PlayHit()
        {
            _heroAnimator.SetTrigger(Hit);
        }


        public void PlayAttack()
        {
            _heroAnimator.SetTrigger(Attack);
        }

        public void PlayDeath()
        {
            _heroAnimator.SetTrigger(Die);
        }


        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _dieStateHash)
                state = AnimatorState.Died;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _runStateHash)
                state = AnimatorState.Walking;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}
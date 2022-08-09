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
        private static readonly int Attack2 = Animator.StringToHash("Attack2");
        private static readonly int Attack3 = Animator.StringToHash("Attack3");
        private static readonly int Move = Animator.StringToHash("Move");

        private readonly int _attackStateHash = Animator.StringToHash("Attack01");
        private readonly int _attackStateHash2 = Animator.StringToHash("Attack02");
        private readonly int _attackStateHash3 = Animator.StringToHash("Attack03");

        private readonly int _dieStateHash = Animator.StringToHash("Dying");

        private readonly int _idleStateHash = Animator.StringToHash("IDLE");
        private readonly int _runStateHash = Animator.StringToHash("Run");
        private CharacterController _characterController;


        private Animator _heroAnimator;

        public AnimatorState State { get; private set; }
   
        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            Debug.Log(State.ToString());
            Debug.Log("Animation Called");
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
            if(State != AnimatorState.Attack && State != AnimatorState.Attack2)
            _heroAnimator.SetTrigger(Attack);

            else if (State == AnimatorState.Attack)
            _heroAnimator.SetTrigger(Attack2);

            else if (State == AnimatorState.Attack2)
            _heroAnimator.SetTrigger(Attack3);
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
            else if (stateHash == _attackStateHash2)
                state = AnimatorState.Attack2;
            else if (stateHash == _attackStateHash3)
                state = AnimatorState.Attack3;
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
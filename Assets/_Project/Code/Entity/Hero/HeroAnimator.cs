using System;
using Code.ConfigData.Ability;
using Code.Entity.Hero.HeroStates;
using Code.Logic.Animator;
using UnityEngine;

namespace Code.Entity.Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Move = Animator.StringToHash("Move");

        private readonly int _attackStateHash = Animator.StringToHash("Attack01");
        private readonly int _attackStateHash2 = Animator.StringToHash("Attack02");
        private readonly int _attackStateHash3 = Animator.StringToHash("Attack03");

        private readonly int _spinAttackStateHash = Animator.StringToHash("SPIN_ATTACK");

        private readonly int _dieStateHash = Animator.StringToHash("Dying");
        private readonly int _idleStateHash = Animator.StringToHash("IDLE");
        private readonly int _runStateHash = Animator.StringToHash("WALKING");
        private readonly int _rollStateHash = Animator.StringToHash("Roll");
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _heroAnimator;
        
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
        
        public void PlayRoll()
        {
            _heroAnimator.CrossFade(_rollStateHash, 0.1f);
        }

        public void PlayRun()
        {
            _heroAnimator.CrossFade(_runStateHash, 0.1f);
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
            return 
                State == AnimatorState.Attack
                   || State == AnimatorState.Attack2
                   || State == AnimatorState.Attack3;
        }

        public void ToIdleState() =>
            _heroAnimator.CrossFade(_idleStateHash, 0.1f);

        public void PlayHit()
        {
            _heroAnimator.SetTrigger(Hit);
        }
        

        public void PlaySpinAttackSkill()
        {
            _heroAnimator.CrossFade(_spinAttackStateHash, 0.1f);
        }

        public void PlayAttack(HeroBaseAbilityState state)
        {
            switch (state)
            {
                case FirstAttackState:
                    _heroAnimator.CrossFade(_attackStateHash, 0.1f);
                    break;
                case SecondAttackState:
                    _heroAnimator.CrossFade(_attackStateHash2, 0.1f);
                    break;
                case ThirdAttackState:
                    _heroAnimator.CrossFade(_attackStateHash3, 0.1f);
                    break;
                case SpinAbilityState:
                    PlaySpinAttackSkill();
                    break;
            }
        }

        public void OverrideController(AnimatorOverrideController controller)
        {
            _heroAnimator.runtimeAnimatorController = controller;
        }
        public void PlayAbilityAnimation(IAbility ability)
        {

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
            else if (stateHash == _rollStateHash)
                state = AnimatorState.Roll;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}
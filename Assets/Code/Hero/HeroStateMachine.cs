using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Code.Hero.HeroStates;
using Code.Services;
using Code.Services.Input;
using Code.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(HeroMovement))]
    public class HeroStateMachine : BaseStateMachine
    {
        private HeroMovement _heroMovement;
        private HeroAnimator _heroAnimator;
        private Dictionary<Type, HeroBaseState> _states;
        private HeroTransition _currentTransition;
        private Dictionary<Type, List<HeroTransition>> _transitions;
        private IInputService _input;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroMovement = GetComponent<HeroMovement>();

            _states = new Dictionary<Type, HeroBaseState>
            {
                [typeof(FirstAttackState)] =
                    new FirstAttackState(this, _input, _heroAnimator),
                [typeof(SecondAttackState)] =
                    new SecondAttackState(this, _input, _heroAnimator),
                [typeof(ThirdAttackState)] =
                    new ThirdAttackState(this, _input, _heroAnimator),
                [typeof(HeroIdleState)] =
                    new HeroIdleState(this, _input, _heroAnimator),
                [typeof(MovementState)] =
                    new MovementState(this, _input, _heroAnimator, _heroMovement),
                [typeof(SpinAttackState)] =
                    new SpinAttackState(this, _input, _heroAnimator),
            };
            //_currentTransitions = new List<HeroTransition>();
            _transitions = new Dictionary<Type, List<HeroTransition>>();

            Func<bool> AttackPressed() => () => _input.isAttackButtonUp();
            Func<bool> FirstSkillPressed() => () => _input.isSkillButton1();
            Func<bool> StateDurationEnd() => () => GetCurrentState().duration <= 0;

            AddTransition(GetState<FirstAttackState>(), GetState<SecondAttackState>(),  AttackPressed());
            AddTransition(GetState<FirstAttackState>(), GetState<SpinAttackState>(), FirstSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());
            AddTransition(GetState<HeroIdleState>(), GetState<SpinAttackState>(),FirstSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<FirstAttackState>(), AttackPressed());
            InitState(_states[typeof(HeroIdleState)]);
        }

        public void AddTransition(HeroBaseState from, HeroBaseState to, Func<bool> condition)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<HeroTransition>();
                _transitions[from.GetType()] = transitions;
            }
            transitions.Add(new HeroTransition(to, condition));
        }

        public void GetTransition(HeroBaseState state)
        {
            foreach (var transition in _transitions[state.GetType()])
            {
                if (transition.Condition())
                {
                    _currentTransition = transition;
                    //ChangeState(transition.To);
                    break;
                }
            }
        }

        public async void DoTransition(HeroBaseState state)
        {
            GetTransition(state);
            await UniTask.WaitUntil(() => state.IsEnded);
            ChangeState(_currentTransition.To);
        }

        async UniTask TransitionTask(int transitionTime,CancellationToken cancellationToken)
        {
            await UniTask.Delay(transitionTime, cancellationToken: cancellationToken);
            
        }

        private IEnumerator TransitionDelay(float transitionTime, HeroTransition transition)
        {
            yield return new WaitForSeconds(transitionTime);
            
            StopAllCoroutines();
        }
        public HeroBaseState GetCurrentState() =>
            CurrentState as HeroBaseState;
        
        public void Enter<TState>() where TState : HeroBaseState => 
            ChangeState(GetState<TState>());
        
        
        public HeroBaseState GetState<TState>() =>
            _states[typeof(TState)];

    }
}

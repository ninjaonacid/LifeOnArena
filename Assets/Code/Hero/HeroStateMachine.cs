using System;
using System.Collections.Generic;
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
        private List<HeroTransition> _transitions;
        private Dictionary<Type, List<HeroTransition>> _possibleTransition;
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
            _possibleTransition = new Dictionary<Type, List<HeroTransition>>();

            InitState(_states[typeof(HeroIdleState)]);
        }

        public void AddTransition(HeroBaseState from, HeroBaseState to, float transitionTime, Func<bool> condition)
        {
            if (_possibleTransition.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<HeroTransition>();
                _possibleTransition[from.GetType()] = transitions;
            }
            transitions.Add(new HeroTransition(to, transitionTime, condition));
        }

        public void MakeTransition(HeroBaseState state)
        {
            foreach (var transition in _possibleTransition[state.GetType()])
            {
                if (transition.Condition())
                {
                    ChangeState(transition.To);
                    break;
                }
            }

        }

        async UniTask TransitionTask(int transitionTime)
        {
            await UniTask.Delay(transitionTime);
        }
        public State GetCurrentState() => CurrentState;
        
        public void Enter<TState>() where TState : HeroBaseState => 
            ChangeState(GetState<TState>());
        
        
        public HeroBaseState GetState<TState>() =>
            _states[typeof(TState)];

    }
}

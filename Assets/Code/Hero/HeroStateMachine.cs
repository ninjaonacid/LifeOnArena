using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Code.Hero.HeroStates;
using Code.Logic;
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
        private HeroSkills _heroSkills;
        private Dictionary<Type, HeroBaseState> _states;
        private HeroTransition _currentTransition;
        private Dictionary<Type, List<HeroTransition>> _transitions;
        private IInputService _input;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroMovement = GetComponent<HeroMovement>();
            _heroSkills = GetComponent<HeroSkills>();

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

            _transitions = new Dictionary<Type, List<HeroTransition>>();

            Func<bool> AttackPressed() => () => _input.isAttackButtonUp();
            Func<bool> FirstSkillPressed() => () => _input.isSkillButton1();
            Func<bool> StateDurationEnd() => () => GetCurrentState().Duration <= 0;
            Func<bool> MovementStart() => () => _input.Axis.sqrMagnitude > Constants.Epsilon;

            AddTransition(GetState<FirstAttackState>(), GetState<SecondAttackState>(),  AttackPressed());
            AddTransition(GetState<FirstAttackState>(), GetState<SpinAttackState>(), FirstSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<SecondAttackState>(), GetState<ThirdAttackState>(),  AttackPressed());
            AddTransition(GetState<SecondAttackState>(), GetState<SpinAttackState>(), FirstSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<HeroIdleState>(), GetState<SpinAttackState>(),FirstSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<MovementState>(), MovementStart());
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
            if (state is HeroBaseAttackState attackState)
            {
                await UniTask.WaitUntil(() => attackState.IsEnded );
            }
            ChangeState(_currentTransition.To);
        }

        public HeroBaseAttackState GetCurrentState() =>
            CurrentState as HeroBaseAttackState;
        
        public void Enter<TState>() where TState : HeroBaseState => 
            ChangeState(GetState<TState>());
        
        public HeroBaseState GetState<TState>() =>
            _states[typeof(TState)];

    }
}

using System;
using System.Collections.Generic;
using Code.Hero.HeroStates;
using Code.Services;
using Code.Services.Input;
using Code.StateMachine;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(HeroMovement))]
    public class HeroStateMachine : BaseStateMachine
    {
        private HeroMovement _heroMovement;
        private HeroAnimator _heroAnimator;
        private HeroRotation _hero;
        private Dictionary<Type, HeroBaseState> _states;
        private List<HeroTransition> _transitions;
        private IInputService _input;

        private void Start()
        {
            _input = AllServices.Container.Single<IInputService>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroMovement = GetComponent<HeroMovement>();
            _hero = GetComponent<HeroRotation>();

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

            };

            _transitions = new List<HeroTransition>()
            {
                new ComboOne(GetState<FirstAttackState>(), GetState<SecondAttackState>()),
                new ComboTwo(GetState<SecondAttackState>(), GetState<ThirdAttackState>()),
            };

            InitState(_states[typeof(HeroIdleState)]);

        }

        public void MakeTransition(HeroBaseState state)
        {
            
        }
        public void Enter<TState>() where TState : HeroBaseState =>
            ChangeState(GetState<TState>());
        
        
        public HeroBaseState GetState<TState>() =>
            _states[typeof(TState)];

    }
}

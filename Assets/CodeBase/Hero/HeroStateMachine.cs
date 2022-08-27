using System;
using System.Collections.Generic;
using CodeBase.Hero.HeroStates;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.StateMachine;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(HeroMovement))]
    public class HeroStateMachine : BaseStateMachine
    {
        private HeroMovement _heroMovement;
        private HeroAnimator _heroAnimator;
        private HeroLookRotation _heroLook;
        private Dictionary<Type, HeroBaseState> _states;
        private IInputService _input;

        private void Start()
        {
            _input = AllServices.Container.Single<IInputService>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroMovement = GetComponent<HeroMovement>();
            _heroLook = GetComponent<HeroLookRotation>();

            _states = new Dictionary<Type, HeroBaseState>

            {
                [typeof(FirstAttackState)] =
                    new FirstAttackState(this, _input, _heroAnimator, _heroLook),
                [typeof(SecondAttackState)] =
                    new SecondAttackState(this, _input, _heroAnimator, _heroLook),
                [typeof(ThirdAttackState)] =
                    new ThirdAttackState(this, _input, _heroAnimator),
                [typeof(HeroIdleState)] =
                    new HeroIdleState(this, _input, _heroAnimator, _heroMovement),
                [typeof(MovementState)] =
                    new MovementState(this, _input, _heroAnimator, _heroMovement),

            };

            InitState(_states[typeof(HeroIdleState)]);

        }

        public void Enter<TState>() where TState : HeroBaseState =>
            ChangeState(GetState<TState>());
        
        
        public HeroBaseState GetState<TState>() =>
            _states[typeof(TState)];

    }
}

using System;
using CodeBase.Hero.HeroStates;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.StateMachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CodeBase.Hero
{
    public class HeroStateMachine : BaseStateMachine
    {
        public PlayerController PlayerController;
        public HeroAttack HeroAttack;
        public HeroAnimator _heroAnimator;
        private IInputService _input;
        private void Start()
        {
            _input = AllServices.Container.Single<IInputService>();

            ChangeState(new HeroIdleState(this, _input, _heroAnimator ));
          
          
        }

       
    }
}

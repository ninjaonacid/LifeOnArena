using System;
using Code.Runtime.Modules.StateMachine.States;
using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected readonly HeroAttackComponent HeroAttackComponent;
        protected readonly HeroWeapon _heroWeapon;

        protected HeroBaseAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
            HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needExitTime = false,
            bool isGhostState = false, 
            Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(heroAnimator, heroMovement, heroRotation,
            needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            HeroAttackComponent = heroAttackComponent;
            _heroWeapon = heroWeapon;
        }
    }
}
using System;
using Code.Runtime.Modules.StateMachine.States;
using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAbilityState : HeroBaseState
    {
        protected readonly HeroWeapon _heroWeapon;
        protected readonly HeroSkills _heroSkills;

        protected HeroBaseAbilityState(HeroWeapon heroWeapon, HeroSkills heroSkills, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(heroAnimator, heroMovement, heroRotation, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _heroWeapon = heroWeapon;
            _heroSkills = heroSkills;
        }
    }
}

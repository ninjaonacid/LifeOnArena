using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;
using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAbilityState : HeroBaseState
    {
        protected readonly HeroWeapon _heroWeapon;
        protected readonly HeroAbilityController _heroAbilityController;
        protected HeroBaseAbilityState(HeroWeapon heroWeapon, HeroAbilityController heroAbilityController, CharacterAnimator characterAnimator,
            HeroMovement heroMovement, HeroRotation heroRotation, AnimationDataContainer animationData,
            bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(characterAnimator, heroMovement, heroRotation,
            animationData, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _heroWeapon = heroWeapon;
            _heroAbilityController = heroAbilityController;
        }
    }
}
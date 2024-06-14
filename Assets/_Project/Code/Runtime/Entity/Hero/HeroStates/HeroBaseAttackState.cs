using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected readonly HeroAttackComponent _heroAttackComponent;
        protected readonly HeroWeapon _heroWeapon;
        protected readonly VisualEffectController _vfxController;
        protected readonly HeroAbilityController _heroAbilityController;
        protected HeroBaseAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
            HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            AnimationDataContainer animationData, VisualEffectController vfxController, HeroAbilityController heroAbilityController, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            heroAnimator, heroMovement, heroRotation, animationData, needExitTime, isGhostState, onEnter, onLogic,
            onExit, canExit)
        {
            _heroAttackComponent = heroAttackComponent;
            _heroWeapon = heroWeapon;
            _vfxController = vfxController;
            _heroAbilityController = heroAbilityController;
        }
    }
}
using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        private VisualEffectController _vfxController;
        public FirstAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
            HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            AnimationDataContainer animationData, VisualEffectController vfxController,
            bool needExitTime = false, 
            bool isGhostState = false,
            Action<State<string, string>> onEnter = null, 
            Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, 
            Func<State<string, string>, bool> canExit = null) : base(
            heroAttackComponent, heroWeapon, heroAnimator, heroMovement, heroRotation, animationData, needExitTime,
            isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _vfxController = vfxController;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _heroWeapon.EnableWeapon(true);
            _heroAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Attack1].Hash);
            _vfxController.PlayVisualEffect(
                _heroWeapon.GetEquippedWeaponData().SlashVisualEffect.VisualEffect.Identifier,
                _heroWeapon.GetEquippedWeapon().SlashVfxTransform,
                 0.15f).Forget();

            _canExit = (state) => true;
            _heroRotation.EnableRotation(false);
        }


        public override void OnExit()
        {
            base.OnExit();
            HeroAttackComponent.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
            _heroRotation.EnableRotation(true);
        }
    }
}
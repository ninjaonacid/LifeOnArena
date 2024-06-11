using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {
        private readonly VisualEffectController _vfxController;
        public ThirdAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
            HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            AnimationDataContainer animationData, VisualEffectController vfxController,
            bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            heroAttackComponent, heroWeapon, heroAnimator, heroMovement, heroRotation, animationData, needExitTime,
            isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _vfxController = vfxController;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Attack3].Hash, 2);
            
            
            _vfxController.PlaySlashVisualEffect(
                _heroWeapon.GetEquippedWeaponData().SlashVisualEffect.VisualEffect.Identifier,
                SlashDirection.Right,
                0.25f).Forget();
            
            _heroRotation.EnableRotation(false);
        }

        public override void OnLogic()
        {
            base.OnLogic();

            if (Timer.Elapsed > _animationData.Animations[AnimationKey.Attack3].Length / 3f)
            {
                _heroWeapon.EnableWeapon(true);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroWeapon.EnableWeapon(false);
            _heroRotation.EnableRotation(true);
            _heroAttackComponent.ClearCollisionData();
            
        }
    }
}
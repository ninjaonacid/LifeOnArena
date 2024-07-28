using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {
        private AttackConfig _attackConfig;
        public ThirdAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
            CharacterAnimator characterAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            AnimationDataContainer animationData, VisualEffectController vfxController,
            HeroAbilityController heroAbilityController, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            heroAttackComponent, heroWeapon, characterAnimator, heroMovement, heroRotation, animationData, vfxController,
            heroAbilityController, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _attackConfig = _heroWeapon.WeaponData.AttacksConfigs[2];

            _characterAnimator.PlayAnimation(_attackConfig.AnimationData.Hash);
            _characterAnimator.SetAttackAnimationSpeed(_attackConfig.AttackAnimationSpeed);
            
            _vfxController.PlaySlashVisualEffect(
                _attackConfig.SlashConfig.VisualEffect.Identifier, _attackConfig.SlashDirection, 
                _attackConfig.SlashConfig.SlashSize, _attackConfig.SlashDelay).Forget();

            _heroRotation.ApplyAttackRotationSpeed();
        }

        public override void OnLogic()
        {
            base.OnLogic();
            
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroRotation.EnableRotation(true);
            _heroWeapon.DisableWeaponCollider();
            _heroAttackComponent.ClearCollisionData();
            _heroRotation.ResetRotationSpeed();
        }
    }
}
using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.Modules.StateMachine.States;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {
        private AttackConfig _attackConfig;
        public ThirdAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
            HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            AnimationDataContainer animationData, VisualEffectController vfxController,
            HeroAbilityController heroAbilityController, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            heroAttackComponent, heroWeapon, heroAnimator, heroMovement, heroRotation, animationData, vfxController,
            heroAbilityController, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _attackConfig = _heroWeapon.WeaponData.AttacksConfigs[2];

            _heroAnimator.PlayAnimation(_attackConfig.AnimationData.Hash);

            _vfxController.PlaySlashVisualEffect(
                _attackConfig.SlashConfig.VisualEffect.Identifier, _attackConfig.SlashDirection, 
                _attackConfig.SlashConfig.SlashSize, _attackConfig.SlashDelay).Forget();

            _heroRotation.EnableRotation(false);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            

            if (Timer.Elapsed >= _attackConfig.ExitTime)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroRotation.EnableRotation(true);
            _heroWeapon.DisableWeaponCollider();
            _heroAttackComponent.ClearCollisionData();
        }
    }
}
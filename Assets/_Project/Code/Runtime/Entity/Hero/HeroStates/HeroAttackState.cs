using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.Modules.StateMachine.States;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class HeroAttackState : HeroBaseAttackState
    {
        private AttackConfig _attackConfig;
        public HeroAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
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
            var attackConfigs = _heroWeapon.WeaponData.AttacksConfigs;
            //_attackConfig = attackConfigs[_heroAbilityController.AttackAbilityCombo % attackConfigs.Count];
            _heroAnimator.PlayAnimation(_attackConfig.AnimationData.Hash);
            _heroAbilityController.TryActivateAbility(_attackConfig.AttackIdentifier);

            
            _vfxController.PlaySlashVisualEffect(
                _attackConfig.SlashConfig.VisualEffect.Identifier, _attackConfig.SlashDirection, 
                _attackConfig.SlashConfig.SlashSize, _attackConfig.SlashDelay).Forget();

            _heroRotation.EnableRotation(false);
        }

        public override void OnLogic()
        {
            base.OnLogic();

            if (Timer.Elapsed >= _attackConfig.AnimationData.Length - 0.3f)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroAttackComponent.ClearCollisionData();
            _heroRotation.EnableRotation(true);
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }
        
      
    }
}
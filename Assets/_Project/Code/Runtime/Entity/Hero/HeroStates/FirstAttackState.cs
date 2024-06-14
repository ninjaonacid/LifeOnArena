using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        public FirstAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon,
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

            _heroWeapon.EnableWeapon(true);
            var attackConfig = _heroWeapon.GetEquippedWeaponData().AttacksConfigs[0];
            _heroAnimator.PlayAnimation(attackConfig.AnimationData.Hash);
            _heroAbilityController.TryActivateAbility(attackConfig.AttackIdentifier);

            
            _vfxController.PlaySlashVisualEffect(
                attackConfig.SlashConfig.VisualEffect.Identifier, attackConfig.SlashDirection, 
                attackConfig.SlashConfig.SlashSize, attackConfig.SlashDelay).Forget();

            _heroRotation.EnableRotation(false);
        }


        public override void OnExit()
        {
            base.OnExit();
            _heroAttackComponent.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
            _heroRotation.EnableRotation(true);
        }
    }
}
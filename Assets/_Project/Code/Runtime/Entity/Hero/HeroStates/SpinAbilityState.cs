using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class SpinAbilityState : HeroBaseAbilityState
    {
        private readonly HeroAttackComponent _heroAttack;
        private float _damageInterval = 0.5f;
        private ITimer _intervalTimer;
        public SpinAbilityState(HeroWeapon heroWeapon, HeroAttackComponent heroAttack, HeroAbilityController heroAbilityController, CharacterAnimator characterAnimator,
            HeroMovement heroMovement, HeroRotation heroRotation, AnimationDataContainer animationData,
            bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(heroWeapon, heroAbilityController, characterAnimator, heroMovement,
            heroRotation, animationData, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _heroAttack = heroAttack;
            _intervalTimer = new Timer();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _characterAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Spinning].Hash);
            
            _heroRotation.EnableRotation(false);
            _heroWeapon.EnableWeaponCollider();
            _heroAttack.ClearCollisionData();
            _intervalTimer.Reset();
        }

        public override void OnLogic()
        {
            base.OnLogic();
            
            _heroMovement.Movement();
            _heroRotation.Rotate(720f);

            if (_intervalTimer.Elapsed > _damageInterval)
            {
                _heroAttack.ClearCollisionData();
            }

        }

        public override void OnExit()
        {
            _heroWeapon.DisableWeaponCollider();
            _heroRotation.EnableRotation(true);
            
        }
    }
}
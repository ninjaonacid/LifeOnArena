using System;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseAttackState
    {
        public SecondAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(heroAttackComponent, heroWeapon, heroAnimator, heroMovement, heroRotation, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayAttack(this);
            _heroWeapon.EnableWeapon(true);
        }
        
        public override void OnExit()
        {
            base.OnExit();
            HeroAttackComponent.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
        }
        
    }
}

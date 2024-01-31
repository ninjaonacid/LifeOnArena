using Code.ConfigData.StateMachine;
using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {
        public ThirdAttackState(HeroAttack heroAttack, HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAttack, heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayAttack(this);
            _heroWeapon.EnableWeapon(true);
            _duration = _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.SecondAttackStateDuration;
        }

        public override void OnLogic()
        {
            _duration -= Time.deltaTime;
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroAttack.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
        }

        public override void OnExitRequest()
        {
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override bool IsStateOver() => _duration <= 0;

    }
}

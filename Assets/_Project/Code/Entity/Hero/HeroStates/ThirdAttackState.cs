using Code.ConfigData.Configs;
using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {
        public ThirdAttackState(HeroAnimator animator, HeroAttack heroAttack, HeroWeapon heroWeapon, bool needExitTime, bool isGhostState) : base(animator, heroAttack, heroWeapon, needExitTime, isGhostState)
        {
        }

     
        public override void OnEnter()
        {
            base.OnEnter();
            _duration = _heroWeapon.WeaponStateMachineConfig.ThirdAttackStateDuration;
            _heroAnimator.PlayAttack(this);
            _heroWeapon.EnableWeapon(true);
        
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

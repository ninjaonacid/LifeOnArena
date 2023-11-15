using Code.ConfigData.Configs;
using Code.Services.AudioService;
using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        public FirstAttackState(HeroAnimator animator, HeroAttack heroAttack, HeroWeapon heroWeapon, bool needExitTime, bool isGhostState) : base(animator, heroAttack, heroWeapon, needExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroWeapon.EnableWeapon(true);
            _heroAnimator.PlayAttack(this);
            _duration = _heroWeapon.WeaponStateMachineConfig.FirstAttackStateDuration;
        }

        public override void OnLogic()
        {
            base.OnLogic();
            _duration -= Time.deltaTime;

            if(IsStateOver()) fsm.StateCanExit(); 

        }

        public override void OnExit()
        {
            base.OnExit();
            _heroWeapon.EnableWeapon(false);
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();

            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override bool IsStateOver() => _duration <= 0;



    }
}

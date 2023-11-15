using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseAttackState
    {
        public SecondAttackState(HeroAttack heroAttack, HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAttack, heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayAttack(this);
            _heroWeapon.EnableWeapon(true);
            _duration = _heroWeapon.WeaponStateMachineConfig.SecondAttackStateDuration;
        }

        public override void OnLogic()
        {
            base.OnLogic();
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

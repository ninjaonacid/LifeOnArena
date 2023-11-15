using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class SpinAbilityState : HeroBaseAttackState
    {
        private readonly HeroRotation _heroRotation;

        public SpinAbilityState(HeroAnimator animator, HeroAttack heroAttack, HeroRotation heroRotation, HeroWeapon heroWeapon, bool needExitTime, bool isGhostState) : base(animator, heroAttack, heroWeapon, needExitTime, isGhostState)
        {
            _heroRotation = heroRotation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroRotation.enabled = false;
            _heroWeapon.EnableWeapon(true);
            _heroAnimator.PlayAttack(this);
            _duration = 0.7f;
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
            _heroRotation.enabled = true;
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

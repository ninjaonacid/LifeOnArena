using Code.ConfigData.Configs;
using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAbilityState
    {
        private HeroFsmConfig _fsmConfig;
        public ThirdAttackState(HeroAnimator animator, HeroFsmConfig config, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
            _fsmConfig = config;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _duration = _fsmConfig.ThirdAttackStateDuration;
            HeroAnimator.PlayAttack(this);
            _heroAttack.SetCollisionOn();
        
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
            _heroAttack.SetCollisionOff();
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

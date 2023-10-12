using Code.ConfigData.Configs;
using Code.Services.AudioService;
using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAbilityState
    {
        private AudioService _audioService;
        private HeroFsmConfig _fsmConfig;
        public FirstAttackState(HeroAnimator animator, HeroFsmConfig config, HeroAttack heroAttack, AudioService audioService, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime, isGhostState)
        {
            _audioService = audioService;
            _fsmConfig = config;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.PlayAttack(this);
            _heroAttack.SetCollisionOn();
            _duration = _fsmConfig.FirstAttackStateDuration;
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
            _heroAttack.SetCollisionOff();
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

using Code.Services.AudioService;
using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAbilityState
    {
        private AudioService _audioService;
        public FirstAttackState(HeroAnimator animator, HeroAttack heroAttack, AudioService audioService, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime, isGhostState)
        {
            _audioService = audioService;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.PlayAttack(this);
            _heroAttack.BaseAttack();
            _duration = 1f;
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

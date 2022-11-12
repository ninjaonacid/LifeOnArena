using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected float Duration;
        protected HeroAttack HeroAttack;
        protected HeroBaseAttackState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator,
            HeroAttack heroAttack) : base(heroStateMachine, input, heroAnimator)
        {
            HeroAttack = heroAttack;
        }

        public bool IsEnded() => Duration <= 0;
    }
}

using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected float Duration;
   
        protected HeroBaseAttackState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public bool IsEnded() => Duration <= 0;
    }
}

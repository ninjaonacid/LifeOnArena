using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected bool IsInTransition;
        public float Duration;
        protected bool _isEnded { get; set; }
        public bool IsEnded { get { return _isEnded; }
            set { _isEnded = value; }
        }
        protected HeroBaseAttackState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }
    }
}

using Code.StateMachine;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public abstract class HeroTransition : BaseTransition
    {
        public abstract float TransitionTime { get; set; }

        protected HeroTransition(State from, State to) : base(from, to)
        {
        }
    }
}

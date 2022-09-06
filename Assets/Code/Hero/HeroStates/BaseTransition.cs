using Code.StateMachine;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public abstract class BaseTransition
    {
        public State To { get; }

        protected BaseTransition(State to)
        {
            To = to;
        }
    }
}

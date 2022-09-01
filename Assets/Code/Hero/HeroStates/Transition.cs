using Code.StateMachine;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public abstract class BaseTransition
    {
        protected readonly State _from;
        protected readonly State _to;

        protected BaseTransition(State from, State to)
        {
            _from = from;
            _to = to;
        }
    }
}

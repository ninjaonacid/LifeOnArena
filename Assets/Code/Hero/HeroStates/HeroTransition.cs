using System;
using Code.StateMachine;

namespace Code.Hero.HeroStates
{
    public class HeroTransition : BaseTransition
    {
        public Func<bool> Condition;

        public HeroTransition(State to, Func<bool> condition) : base(to)
        {
            Condition = condition;
        }
    }
}

using Code.StateMachine;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class ComboOne : HeroTransition
    {
        public override float TransitionTime { get; set; }

        public ComboOne(State from, State to) : base(from, to)
        {
            
        }
    }
}

using Code.StateMachine;

namespace Code.Hero.HeroStates
{
    public class ComboTwo : HeroTransition
    {
        public override float TransitionTime { get; set; }

        public ComboTwo(State from, State to) : base(from, to)
        {
        }
    }
}

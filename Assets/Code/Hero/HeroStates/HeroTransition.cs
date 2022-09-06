using System;
using Code.StateMachine;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class HeroTransition : BaseTransition
    {
        public float TransitionTime { get; }
        public Func<bool> Condition;

        public HeroTransition(State to, float transitionTime, Func<bool> condition) : base(to)
        {
            TransitionTime = transitionTime;
            Condition = condition;
        }
    }
}

using System;
using UnityEngine;

namespace Code.Runtime.Logic.Animator
{
    public class AnimationEventReceiver : MonoBehaviour
    {
        public event Action AnimationStart;
        public event Action AnimationEnd;
        public void AnimationStartEvent()
        {
            AnimationStart?.Invoke();
        }

        public void AnimationEndEvent()
        {
            AnimationEnd?.Invoke();
        }
    }
}

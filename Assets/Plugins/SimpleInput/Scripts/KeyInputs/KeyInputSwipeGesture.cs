using UnityEngine;

namespace SimpleInputNamespace
{
    public class KeyInputSwipeGesture : SwipeGestureBase<KeyCode, bool>
    {
        public SimpleInput.KeyInput key = new SimpleInput.KeyInput();

        protected override BaseInput<KeyCode, bool> Input => key;
        protected override bool Value => true;

        public override int Priority => 1;
    }
}
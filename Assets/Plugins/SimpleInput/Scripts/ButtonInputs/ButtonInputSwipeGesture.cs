namespace SimpleInputNamespace
{
    public class ButtonInputSwipeGesture : SwipeGestureBase<string, bool>
    {
        public SimpleInput.ButtonInput button = new SimpleInput.ButtonInput();

        protected override BaseInput<string, bool> Input => button;
        protected override bool Value => true;

        public override int Priority => 1;
    }
}
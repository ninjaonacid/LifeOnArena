namespace SimpleInputNamespace
{
    public class MouseButtonInputSwipeGesture : SwipeGestureBase<int, bool>
    {
        public SimpleInput.MouseButtonInput mouseButton = new SimpleInput.MouseButtonInput();

        protected override BaseInput<int, bool> Input => mouseButton;
        protected override bool Value => true;

        public override int Priority => 1;
    }
}
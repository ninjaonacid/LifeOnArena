namespace SimpleInputNamespace
{
    public class AxisInputSwipeGesture : SwipeGestureBase<string, float>
    {
        public SimpleInput.AxisInput axis = new SimpleInput.AxisInput();
        public float value = 1f;

        protected override BaseInput<string, float> Input => axis;
        protected override float Value => value;

        public override int Priority => 1;
    }
}
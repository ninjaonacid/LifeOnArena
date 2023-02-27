using UnityEngine;

namespace Code.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                if (_isDisabled)
                {
                    return Vector2.zero;
                }
                
                return SimpleInputAxis();
            }
        }

    }
}
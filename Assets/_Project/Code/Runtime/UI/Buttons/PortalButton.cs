using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public class PortalButton : AnimatedButton
    {
        private ArrowUI _arrowUI;

        [SerializeField] private float _movementDistance;
        public void AnimateArrow()
        {
            var movementDirection = _arrowUI.transform.forward;
            
           _arrowUI.Movement(_movementDistance * movementDirection);
        }
    }
}

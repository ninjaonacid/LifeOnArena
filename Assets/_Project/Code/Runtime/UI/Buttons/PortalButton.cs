using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public class PortalButton : AnimatedButton
    {
        [SerializeField] private ArrowUI _arrowUI;
        [SerializeField] private float _movementDistance;
        
        public void AnimateArrow()
        {
            Vector2 movementDirection = _arrowUI.RectTransform.up;
            var arrowPos = _arrowUI.RectTransform.anchoredPosition;
            Vector2 newPos = arrowPos + movementDirection * _movementDistance;
           _arrowUI.Movement(newPos);
        }
    }
}

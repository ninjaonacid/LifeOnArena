using PrimeTween;
using UnityEngine;

namespace Code.Runtime.UI
{
    public class ArrowUI : CanvasElement
    {
        public void Movement(Vector2 movementDirection)
        {
            RectTransform.DOAnchorPos(movementDirection, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetLink(this.gameObject);
        }
        
    }
}

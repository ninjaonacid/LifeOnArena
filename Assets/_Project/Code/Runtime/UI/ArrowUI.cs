using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.UI
{
    public class ArrowUI : CanvasElement
    {
        private RectTransform _transform;

        protected override void Awake()
        {
            base.Awake();
            _transform = GetComponent<RectTransform>();
        }

        public void Movement(Vector2 movementDirection)
        {
            _transform.DOAnchorPos(movementDirection, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetLink(this.gameObject);
        }
    }
}

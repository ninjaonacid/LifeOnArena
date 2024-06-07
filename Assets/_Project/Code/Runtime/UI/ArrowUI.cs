using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.UI
{
    public class ArrowUI : MonoBehaviour
    {
        private RectTransform _transform;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void Movement(Vector3 movementDirection)
        {
            _transform.DOAnchorPos(movementDirection, 1).SetLoops(-1, LoopType.Yoyo).SetLink(this.gameObject);
        }
    }
}

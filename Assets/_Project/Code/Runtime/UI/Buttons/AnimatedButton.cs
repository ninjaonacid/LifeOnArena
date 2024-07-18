using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public class AnimatedButton : BaseButton
    {
        [SerializeField] private Vector2 _animationScale;
        [SerializeField] private float _duration;
        
        private Tween _tween;
        private Vector3 _baseScale;
        private RectTransform _rectTransform;

        protected override void Awake()
        {
            base.Awake();
            _rectTransform = (RectTransform)transform;
            _baseScale = _rectTransform.localScale;
        }

        public void PlayScaleAnimation()
        {
            ResetState();
            
            if (_tween != null)
            {
                _tween.Complete();
                _tween.Kill();
            }
            
            _tween = _rectTransform.DOScale(new Vector3(_baseScale.x + _animationScale.x,
                _baseScale.y + _animationScale.y), _duration)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBack).SetLink(gameObject);
        }

        public void StopAnimation()
        {
            ResetState();
            _tween.Complete();
            _tween.Kill();
        }

        private void ResetState()
        {
            _rectTransform.localScale = _baseScale;
        }
    }
}
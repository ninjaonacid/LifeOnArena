using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public class AnimatedButton : BaseButton
    {
        [SerializeField] private Vector3 _animationScale;
        [SerializeField] private float _duration;
        private Tween _tween;
        private Vector3 _baseScale;

        public void Start()
        {
            _baseScale = transform.localScale;
        }

        public void PlayScaleAnimation()
        {
            if (_tween != null)
            {
                _tween.Complete();
                _tween.Kill();
            }
            
            _tween = transform.DOScale(new Vector3(_baseScale.x + _animationScale.x,
                _baseScale.y + _animationScale.y, _baseScale.z + _animationScale.z), _duration)
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
            transform.localScale = _baseScale;
        }
    }
}
using PrimeTween;
using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public class AnimatedButton : BaseButton
    {
        [SerializeField] private Vector2 _animationScale;
        [SerializeField] private float _duration;

        private Tween _tween;
        private Vector3 _baseScale;


        protected override void Awake()
        {
            base.Awake();
            _baseScale = RectTransform.localScale;
        }

        public void PlayScaleAnimation()
        {
            ResetState();

            _tween.Complete();
            
            _tween = RectTransform.DOScale(new Vector3(_animationScale.x,
                    _animationScale.y), _duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.OutBack)
                .SetLink(gameObject);
        }

        public void StopAnimation()
        {
            ResetState();
            _tween.Complete();
        }

        private void ResetState()
        {
            RectTransform.localScale = _baseScale;
        }
    }
}
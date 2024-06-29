
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Modules.WindowAnimations
{
    public class ScaleAnimation : WindowAnimation
    {
        private Tween _tween;
        public void Awake()
        {
            _windowRectTransform.localPosition = Vector3.zero;
            _windowRectTransform.localScale = Vector3.zero;
        }

        public override void ShowAnimation()
        {
            if (_tween != null) _tween.Kill();
            
            _tween = _windowRectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutExpo).SetLink(gameObject);
        }

        public override void CloseAnimation()
        {
            if(_tween != null) _tween.Kill();

            _tween = _windowRectTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject)).SetLink(gameObject);
        }

        public override void HideAnimation()
        {
            throw new System.NotImplementedException();
        }
    }
}
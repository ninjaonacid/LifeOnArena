using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    public class BaseButton : CanvasElement, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField] private Color32 _highlightColor;
        [SerializeField] private Color32 _pressedColor;
        [SerializeField] private Color32 _baseColor;
        
        [SerializeField] private Image _buttonImage;

        private Subject<PointerEventData> _subject;


        protected override void Awake()
        {
            base.Awake();
            _buttonImage ??= GetComponent<Image>();
            _baseColor = new Color32(255, 255, 255, 255);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnClickAsObservable()
        {
            return _subject ??= (_subject = new Subject<PointerEventData>());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetColor(_pressedColor);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetColor(_baseColor);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetColor(_highlightColor);
        }

        private void SetColor(Color32 color)
        {
            _buttonImage.color = color;
        }
    }
}
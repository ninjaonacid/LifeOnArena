using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.UI.Buttons
{
    public class BaseButton : MonoBehaviour, IPointerClickHandler
    {
        private Subject<PointerEventData> _subject;

        [SerializeField] private CanvasGroup _canvasGroup;
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnClickAsObservable()
        {
            return _subject ??= (_subject = new Subject<PointerEventData>());
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }

      
        
        
    }
}
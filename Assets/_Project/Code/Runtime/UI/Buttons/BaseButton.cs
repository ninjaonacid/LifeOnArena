using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.UI.Buttons
{
    public class BaseButton : CanvasElement, IPointerClickHandler
    {
        private Subject<PointerEventData> _subject;
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(eventData);
        }

        public IObservable<PointerEventData> OnClickAsObservable()
        {
            return _subject ??= (_subject = new Subject<PointerEventData>());
        }
        
    }
}
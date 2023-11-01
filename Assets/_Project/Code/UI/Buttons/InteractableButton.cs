using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.Buttons
{
    public abstract class InteractableButton : MonoBehaviour, IPointerClickHandler
    {
        private Subject<PointerEventData> _subject;

        [SerializeField] private CanvasGroup _canvasGroup;

        public void OnPointerClick(PointerEventData eventData)
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
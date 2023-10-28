using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.Buttons
{
    public abstract class InteractableButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnButtonPressed;
        private Subject<PointerEventData> _subject = new Subject<PointerEventData>();
        public IObservable<PointerEventData> Observable;

        [SerializeField] private CanvasGroup _canvasGroup;

        public void OnPointerClick(PointerEventData eventData)
        {
            _subject.OnNext(eventData);
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }
}
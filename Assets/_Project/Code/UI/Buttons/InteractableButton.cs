using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.Buttons
{
    public abstract class InteractableButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnButtonPressed;
        
        [SerializeField] private CanvasGroup _canvasGroup;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnButtonPressed?.Invoke();
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }
}
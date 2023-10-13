using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.SkillsMenu
{
    public class UnEquipSkillButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public event Action OnUnEquipButtonPressed;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnUnEquipButtonPressed?.Invoke();
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
            _canvasGroup.interactable = value;
        }
    }
}

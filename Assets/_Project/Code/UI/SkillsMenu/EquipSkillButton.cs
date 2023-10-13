using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.SkillsMenu
{
    public class EquipSkillButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public event Action OnEquipButtonPressed;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnEquipButtonPressed?.Invoke();
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
        }
    }
}

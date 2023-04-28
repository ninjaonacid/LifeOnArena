using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    public class EquipSkillButton : MonoBehaviour, IPointerClickHandler
    {
        public Button EquipButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
        }
    }
}

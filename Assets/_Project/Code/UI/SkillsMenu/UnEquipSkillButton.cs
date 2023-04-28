using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.SkillsMenu
{
    public class UnEquipSkillButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;


        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
            _canvasGroup.interactable = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}

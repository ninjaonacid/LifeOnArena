using Code.Data;
using Code.Data.PlayerData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.SkillsMenu
{
    public class EquipSkillButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private SkillSlotsData _slotsData;
        private UISkillPanelController _controller;
        public void Construct(UISkillPanelController controller, SkillSlotsData slotsData)
        {
            _slotsData = slotsData;
            _controller = controller;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _controller.EquipSkill();
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
        }
    }
}

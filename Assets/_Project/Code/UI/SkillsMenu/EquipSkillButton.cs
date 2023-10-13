using Code.Data.PlayerData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.SkillsMenu
{
    public class EquipSkillButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private SkillSlotsData _slotsData;
        private UISkillPanelContainer _container;
        public void Construct(UISkillPanelContainer container, SkillSlotsData slotsData)
        {
            _slotsData = slotsData;
            _container = container;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            _container.EquipSkill();
        }

        public void ButtonClicked()
        {
            
        }

        public void ShowButton(bool value)
        {
            _canvasGroup.alpha = value ? 255 : 0;
        }
    }
}

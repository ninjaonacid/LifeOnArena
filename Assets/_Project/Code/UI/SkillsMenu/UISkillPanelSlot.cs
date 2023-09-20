using Code.ConfigData.Ability;
using Code.Services.PersistentProgress;
using Code.UI.HUD.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Code.UI.SkillsMenu
{
    public class UISkillPanelSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _skillIcon;
        [SerializeField] private CanvasGroup _selectionFrame;
        [SerializeField] private CanvasGroup _equippedFrame;
        [SerializeField] private AbilityTemplateBase _abilitySO;
        [SerializeField] private TextMeshProUGUI _slotNumber;

        public bool IsEquipped = false;
        private IGameDataContainer _gameData;
        private UISkillPanelController _controller;
        public void Construct(IGameDataContainer gameData, UISkillPanelController controller)
        {
            _gameData = gameData;
            _controller = controller;

            SetIcon();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _controller.SetSelectedSlot(this);
        }

        private void SetIcon()
        {
            _skillIcon.sprite = _abilitySO?.Icon;
        }
        public void ShowSelectionFrame(bool value)
        {
            _selectionFrame.alpha = value ? 255 : 0;
        }

        public void SetSlot(AbilitySlotID id)
        {
            _slotNumber.text = ((int)id).ToString();
        }

        public AbilityTemplateBase GetAbility() =>
            _abilitySO;

    }
}

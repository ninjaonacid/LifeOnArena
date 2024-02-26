using Code.Runtime.ConfigData.Ability;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.View.HUD.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Code.Runtime.UI.AbilityMenu
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
        private UISkillPanelContainer _container;
        public void Construct(IGameDataContainer gameData, UISkillPanelContainer container)
        {
            _gameData = gameData;
            _container = container;

            SetIcon();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _container.SetSelectedSlot(this);
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

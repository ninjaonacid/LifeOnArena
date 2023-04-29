using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
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
        private IProgressService _progress;
        private UISkillPanelController _controller;
        public void Construct(IProgressService progress, UISkillPanelController controller)
        {
            _progress = progress;
            _controller = controller;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _controller.SetSelectedSlot(this);
        }

        public void ShowSelectionFrame(bool value)
        {
            if (value)
            {
                _selectionFrame.alpha = 255;
            }
            else
            {
                _selectionFrame.alpha = 0;
            }
        }

        public void SetSlotNumber(int value)
        {
            _slotNumber.text = value.ToString();
        }

        public AbilityTemplateBase GetAbility() =>
            _abilitySO;

    }
}

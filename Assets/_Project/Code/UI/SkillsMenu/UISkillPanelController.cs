using Code.Services.PersistentProgress;
using Code.UI.HUD.Skills;
using UnityEngine;
using VContainer;

namespace Code.UI.SkillsMenu
{
    public class UISkillPanelController : MonoBehaviour
    {
        [SerializeField] private UISkillPanelSlot[] _slots;
        [SerializeField] private EquipSkillButton _equipButton;
        [SerializeField] private UnEquipSkillButton _unEquipButton;
        private UISkillPanelSlot _selectedSlot;
        private IProgressService _progress;

        [Inject]
        public void Construct(IProgressService progress)
        {
            _progress  = progress;
            foreach (UISkillPanelSlot slot in _slots)
            {
                slot.Construct(progress, this);
            }
            _equipButton.Construct(this, _progress.Progress.SkillSlotsData);
        }
        public void SetSelectedSlot(UISkillPanelSlot slot)
        {
            if (_selectedSlot != null && _selectedSlot != slot)
            {
                _selectedSlot.ShowSelectionFrame(false);
                _selectedSlot = slot;
                _selectedSlot.ShowSelectionFrame(true);
                ShowInteractButton(slot);
            }
            else
            {
                _selectedSlot = slot;
                _selectedSlot.ShowSelectionFrame(true);
                ShowInteractButton(slot);
            }
        }

        private void ShowInteractButton(UISkillPanelSlot slot)
        {
            if (slot.IsEquipped)
            {
                _equipButton.ShowButton(false);
                _unEquipButton.ShowButton(true);
            }
            else
            {
                _equipButton.ShowButton(true);
                _unEquipButton.ShowButton(false);
            }
        }
        
        public void EquipSkill()
        {
            var hudSkills = _progress.Progress.SkillSlotsData.SlotSkill;

            var slot = _progress.Progress.SkillSlotsData.FindSlot();
            hudSkills[slot] = _selectedSlot.GetAbility().Id;
            _selectedSlot.IsEquipped = true;
            _selectedSlot.SetSlotNumber((int)(slot));

            if (hudSkills[AbilitySlotID.Second] != null)
            {
               
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Code.Services.PersistentProgress;
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
        private readonly Queue<UISkillPanelSlot> _equippedSlots = new Queue<UISkillPanelSlot>(2);

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
            var hudSkills = _progress.Progress.SkillSlotsData;

            if (!_equippedSlots.Contains(_selectedSlot) && _equippedSlots.Count < 2)
            {
                _equippedSlots.Enqueue(_selectedSlot);
                _selectedSlot.IsEquipped = true;
            }
            else if (!_equippedSlots.Contains(_selectedSlot) && _equippedSlots.Count >= 2)
            {
                var firstSlot = _equippedSlots.Dequeue();
                firstSlot.IsEquipped = false;
                _equippedSlots.Enqueue(_selectedSlot);
            }
          

            hudSkills.SkillIds.Clear();

            foreach (var slot in _equippedSlots)
            {
                if(!hudSkills.SkillIds.Contains(slot.GetAbility().Id))
                    hudSkills.SkillIds.Enqueue(slot.GetAbility().Id);
            }
        }
    }
}

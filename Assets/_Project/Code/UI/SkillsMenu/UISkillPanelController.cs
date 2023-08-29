using System.Collections.Generic;
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

        private IGameDataContainer _gameData;

        [Inject]
        public void Construct(IGameDataContainer gameData)
        {
            _gameData  = gameData;
            
            foreach (UISkillPanelSlot slot in _slots)
            {
                slot.Construct(gameData, this);
            }
            
            _equipButton.Construct(this, _gameData.PlayerData.SkillSlotsData);
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
            var hudSkills = _gameData.PlayerData.SkillSlotsData;

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
                hudSkills.SkillIds.Enqueue(slot.GetAbility().Identifier.Id);
            }
        }
    }
}

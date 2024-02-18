using System.Collections.Generic;
using Code.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace Code.UI.AbilityMenu
{
    public class UISkillPanelContainer : MonoBehaviour
    {
        [SerializeField] private UISkillPanelSlot[] _slots;
        public EquipSkillButton EquipButton;
        [SerializeField] private UnEquipSkillButton _unEquipButton;

        private UISkillPanelSlot _selectedSlot;
        private readonly Queue<UISkillPanelSlot> _equippedSlots = new Queue<UISkillPanelSlot>(2);

        private IGameDataContainer _gameData;

        [Inject]
        public void Construct(IGameDataContainer gameData)
        {
            _gameData = gameData;

            foreach (UISkillPanelSlot slot in _slots)
            {
                slot.Construct(gameData, this);
            }

            // EquipButton.Construct(this, _gameData.PlayerData.SkillSlotsData);
        }

        public void SetSelectedSlot(UISkillPanelSlot slot)
        {
            if (_selectedSlot != null)
            {
                _selectedSlot.ShowSelectionFrame(false);
            }

            _selectedSlot = slot;
            _selectedSlot.ShowSelectionFrame(true);
            ShowInteractButton(slot);
        }

        private void ShowInteractButton(UISkillPanelSlot slot)
        {
            EquipButton.ShowButton(!slot.IsEquipped);
            _unEquipButton.ShowButton(slot.IsEquipped);
        }

        public void EquipSkill()
        {
            var hudSkills = _gameData.PlayerData.AbilityData;

            if (!_equippedSlots.Contains(_selectedSlot) && _equippedSlots.Count < 2)
            {
                _equippedSlots.Enqueue(_selectedSlot);
                _selectedSlot.IsEquipped = true;
                ShowInteractButton(_selectedSlot);
            }
            else if (!_equippedSlots.Contains(_selectedSlot) && _equippedSlots.Count >= 2)
            {
                var firstSlot = _equippedSlots.Dequeue();
                firstSlot.IsEquipped = false;
                ShowInteractButton(firstSlot);
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
using System;
using System.Collections.Generic;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;
using Code.Utils;

namespace Code.UI.Model.AbilityMenu
{
    [Serializable]
    public class AbilityMenuModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        private readonly IConfigProvider _configProvider;
        private List<UIAbilitySlotModel> _abilitySlots;
        private Queue<UIAbilitySlotModel> _equippedSlots = new Queue<UIAbilitySlotModel>();

        public AbilityMenuModel(IGameDataContainer gameData, IConfigProvider configProvider)
        {
            _gameData = gameData;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _abilitySlots = new List<UIAbilitySlotModel>();

            var allAbilities = _configProvider.AllAbilities();

            foreach (var ability in allAbilities)
            {
                var abilitySlotModel = new UIAbilitySlotModel(ability.Identifier.Name)
                {
                    Ability = ability
                };
                
                _abilitySlots.Add(abilitySlotModel);
            }

            if (_gameData.PlayerData.SkillSlotsData.AbilitySlots.Count <= 0) return;
            for (var index = 0; index < _abilitySlots.Count; index++)
            {
                var abilitySlot = _abilitySlots[index];
                abilitySlot.IsEquipped = _gameData.PlayerData.SkillSlotsData.AbilitySlots[index].IsEquipped;
            }
        }

        public int GetEquippedSlotIndex(UIAbilitySlotModel slot)
        {
            return _equippedSlots.IndexOf(slot) + 1;
        }
        

        public UIAbilitySlotModel GetSlotByIndex(int index)
        {
            return _abilitySlots[index];
        }

        public bool IsSlotEquipped(int index)
        {
            return _abilitySlots[index].IsEquipped;
        }

        public void UnEquipAbility(int slotIndex)
        {
            _abilitySlots[slotIndex].IsEquipped = false;

            _equippedSlots.Dequeue();
        }

        public void EquipAbility(int slotIndex)
        {
            _abilitySlots[slotIndex].IsEquipped = true;
            if (_equippedSlots.Count < 2)
            {
                _equippedSlots.Enqueue(_abilitySlots[slotIndex]);
            }
            else
            {
                var deletedAbility = _equippedSlots.Dequeue();
                deletedAbility.IsEquipped = false;
                _equippedSlots.Enqueue(_abilitySlots[slotIndex]);
            }

            _gameData.PlayerData.SkillSlotsData.SkillIds.Enqueue(_abilitySlots[slotIndex].Ability.Identifier.Id);
        }
        
        
        public List<UIAbilitySlotModel> GetSlots()
        {
            return _abilitySlots;
        }
        
    }
}

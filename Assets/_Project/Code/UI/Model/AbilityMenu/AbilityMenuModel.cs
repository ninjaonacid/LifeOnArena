using System;
using System.Collections.Generic;
using Code.Data.DataStructures;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;

namespace Code.UI.Model.AbilityMenu
{
    [Serializable]
    public class AbilityMenuModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        private readonly IConfigProvider _configProvider;
        private List<UIAbilitySlotModel> _abilitySlots;
        private IndexedQueue<UIAbilitySlotModel> _equippedSlots;
        public AbilityMenuModel(IGameDataContainer gameData, IConfigProvider configProvider)
        {
            _gameData = gameData;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _abilitySlots = new List<UIAbilitySlotModel>();
            _equippedSlots = new IndexedQueue<UIAbilitySlotModel>();

            var allAbilities = _configProvider.AllAbilities();

            foreach (var ability in allAbilities)
            {
                var abilitySlotModel = new UIAbilitySlotModel()
                {
                    Ability = ability,
                    AbilityName = ability.Identifier.Name,
                    AbilityId = ability.Identifier.Id
                };
                
                _abilitySlots.Add(abilitySlotModel);
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
            var ability = _abilitySlots[slotIndex];
            ability.IsEquipped = false;

            _equippedSlots.Remove(ability);
        }

        public void EquipAbility(int slotIndex)
        {
            var ability = _abilitySlots[slotIndex];

            ability.IsEquipped = true;
            
            if (_equippedSlots.Count < 2)
            {
                _equippedSlots.Enqueue(ability);
            }
            else
            {
                var deletedAbility = _equippedSlots.Dequeue();
                deletedAbility.IsEquipped = false;
                _equippedSlots.Enqueue(ability);
            }
        }

        public List<UIAbilitySlotModel> GetSlots()
        {
            return _abilitySlots;
        }

        public void LoadData()
        {
            if (_gameData.PlayerData.AbilityData.AbilitySlots.Count <= 0) return;
            
            for (var index = 0; index < _abilitySlots.Count; index++)
            {
                var abilitySlot = _abilitySlots[index];
                
                abilitySlot.IsEquipped = _gameData.PlayerData.AbilityData.AbilitySlots[index].IsEquipped;
            }

            _equippedSlots = new IndexedQueue<UIAbilitySlotModel>(_gameData.PlayerData.AbilityData.EquippedSlots);
        }

        public void SaveModelData()
        {
            _gameData.PlayerData.AbilityData.AbilitySlots = new List<UIAbilitySlotModel>(_abilitySlots);
            _gameData.PlayerData.AbilityData.EquippedSlots = new List<UIAbilitySlotModel>(_equippedSlots);
        }
    }
}

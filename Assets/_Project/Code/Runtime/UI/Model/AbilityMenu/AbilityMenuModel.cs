using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Data.DataStructures;
using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model.AbilityMenu
{
    [Serializable]
    public class AbilityMenuModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        private readonly IConfigProvider _configProvider;
        private List<UIAbilityModel> _abilitySlots;
        private IndexedQueue<UIAbilityModel> _equippedAbilities;

        public AbilityMenuModel(IGameDataContainer gameData, IConfigProvider configProvider)
        {
            _gameData = gameData;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _abilitySlots = new List<UIAbilityModel>();
            _equippedAbilities = new IndexedQueue<UIAbilityModel>();

            var allAbilities = _configProvider.AllAbilities().OrderBy(x => x.Price);
            

            foreach (var ability in allAbilities)
            {
                var abilitySlotModel = new UIAbilityModel()
                {
                    ActiveAbilityBlueprintBase = ability,
                    AbilityName = ability.Identifier.Name,
                    AbilityId = ability.Identifier.Id,
                    Price = ability.Price
                };

                if (abilitySlotModel.Price == 0)
                {
                    abilitySlotModel.IsUnlocked = true;
                }

                _abilitySlots.Add(abilitySlotModel);
            }
        }

        public int GetEquippedSlotIndex(UIAbilityModel slot)
        {
            return _equippedAbilities.FindIndex(0,
                _equippedAbilities.Count, x => x.AbilityId == slot.AbilityId) + 1;
        }

        public UIAbilityModel GetSlotByIndex(int index)
        {
            return _abilitySlots[index];
        }

        public bool IsAbilityEquipped(int index)
        {
            return _abilitySlots[index].IsEquipped;
        }

        public bool IsAbilityUnlocked(int index)
        {
            return _abilitySlots[index].IsUnlocked;
        }

        public void UnlockAbility(int index)
        {
            var ability = _abilitySlots[index];

            if (_gameData.PlayerData.WorldData.LootData.Collected >= ability.Price)
            {
                ability.IsUnlocked = true;
            }
        }

        public void UnEquipAbility(int slotIndex)
        {
            var ability = _abilitySlots[slotIndex];
            ability.IsEquipped = false;

            _equippedAbilities.Remove(ability);
        }

        public void EquipAbility(int slotIndex)
        {
            var ability = _abilitySlots[slotIndex];

            ability.IsEquipped = true;

            if (_equippedAbilities.Count < 2)
            {
                _equippedAbilities.Enqueue(ability);
            }
            else
            {
                var deletedAbility = _equippedAbilities.Dequeue();
                deletedAbility.IsEquipped = false;
                _equippedAbilities.Enqueue(ability);
            }
        }

        public List<UIAbilityModel> GetSlots()
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
                abilitySlot.IsUnlocked = _gameData.PlayerData.AbilityData.AbilitySlots[index].IsUnlocked;
            }

            foreach (var slot in _gameData.PlayerData.AbilityData.EquippedSlots)
            {
                var ability = _abilitySlots.FirstOrDefault(x => x.AbilityId == slot.AbilityId);
                _equippedAbilities.Enqueue(ability);
            }
            
        }

        public void SaveModelData()
        {
            _gameData.PlayerData.AbilityData.AbilitySlots = new List<UIAbilityModel>(_abilitySlots);
            _gameData.PlayerData.AbilityData.EquippedSlots = new List<UIAbilityModel>(_equippedAbilities);
        }
    }
}
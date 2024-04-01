using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Data.DataStructures;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model.AbilityMenu
{
    [Serializable]
    public class AbilityTreeWindowModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        private readonly IConfigProvider _configProvider;
        private List<UIAbilityModel> _abilities;
        private IndexedQueue<UIAbilityModel> _equippedAbilities;

        public AbilityTreeWindowModel(IGameDataContainer gameData, IConfigProvider configProvider)
        {
            _gameData = gameData;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _abilities = new List<UIAbilityModel>();
            _equippedAbilities = new IndexedQueue<UIAbilityModel>();

            var allAbilities =
                _configProvider.AllAbilities()
                    .OrderBy(x => x.AbilityTreeData.Position);
                    
            
            foreach (var ability in allAbilities)
            {
                var abilitySlotModel = new UIAbilityModel()
                {
                    ActiveAbilityBlueprintBase = ability,
                    AbilityName = ability.Identifier.Name,
                    AbilityId = ability.Identifier.Id,
                    Price = ability.AbilityTreeData.Price
                };

                if (abilitySlotModel.Price == 0)
                {
                    abilitySlotModel.IsUnlocked = true;
                }

                _abilities.Add(abilitySlotModel);
            }
        }

        public int GetEquippedSlotIndex(UIAbilityModel slot)
        {
            return _equippedAbilities.FindIndex(0,
                _equippedAbilities.Count, x => x.AbilityId == slot.AbilityId) + 1;
        }

        public UIAbilityModel GetSlotByIndex(int index)
        {
            return _abilities[index];
        }

        public bool IsAbilityEquipped(int index)
        {
            return _abilities[index].IsEquipped;
        }

        public bool IsAbilityUnlocked(int index)
        {
            return _abilities[index].IsUnlocked; }

        public void UnlockAbility(int index)
        {
            var ability = _abilities[index];

            if (index > 0)
            {
                var previousAbility = _abilities[index - 1];
                
                if (!previousAbility.IsUnlocked) return;
                
                if (_gameData.PlayerData.WorldData.LootData.Collected >= ability.Price)
                {
                    ability.IsUnlocked = true;
                }
            }
            
            
        }

        public void UnEquipAbility(int slotIndex)
        {
            var ability = _abilities[slotIndex];
            ability.IsEquipped = false;

            _equippedAbilities.Remove(ability);
        }

        public void EquipAbility(int slotIndex)
        {
            var ability = _abilities[slotIndex];

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
            return _abilities;
        }

        public void LoadData()
        {
            if (_gameData.PlayerData.AbilityData.UnlockedAbilities.Count <= 0) return;

            for (var index = 0; index < _abilities.Count; index++)
            {
                var abilitySlot = _abilities[index];

                abilitySlot.IsEquipped = _gameData.PlayerData.AbilityData.UnlockedAbilities[index].IsEquipped;
                abilitySlot.IsUnlocked = _gameData.PlayerData.AbilityData.UnlockedAbilities[index].IsUnlocked;
            }

            foreach (var slot in _gameData.PlayerData.AbilityData.EquippedAbilities)
            {
                var ability = _abilities.FirstOrDefault(x => x.AbilityId == slot.AbilityId);
                _equippedAbilities.Enqueue(ability);
            }
            
        }

        public void SaveModelData()
        {
            _gameData.PlayerData.AbilityData.UnlockedAbilities = new List<UIAbilityModel>(_abilities);
            _gameData.PlayerData.AbilityData.EquippedAbilities = new List<UIAbilityModel>(_equippedAbilities);
        }
    }
}
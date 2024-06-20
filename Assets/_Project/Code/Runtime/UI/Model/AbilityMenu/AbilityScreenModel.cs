using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Core.Config;
using Code.Runtime.Data.DataStructures;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Services.PersistentProgress;
using UniRx;

namespace Code.Runtime.UI.Model.AbilityMenu
{
    [Serializable]
    public class AbilityScreenModel : IScreenModel
    {
        public ReactiveProperty<int> Souls;

        private readonly IGameDataContainer _gameData;
        private readonly ConfigProvider _configProvider;
        private List<AbilityModel> _abilities;
        private IndexedQueue<AbilityModel> _equippedAbilities;

        public AbilityScreenModel(IGameDataContainer gameData, ConfigProvider configProvider)
        {
            _gameData = gameData;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var souls = _gameData.PlayerData.WorldData.LootData.Collected;
            Souls = new ReactiveProperty<int>(souls);
            
            _abilities = new List<AbilityModel>();
            _equippedAbilities = new IndexedQueue<AbilityModel>();

            var allAbilities =
                _configProvider.AllAbilities()
                    .Where(x => x.AbilityTreeData != null && x.AbilityTreeData.Branch != AbilityTreeBranch.NotInTree)
                    .OrderBy(x => x.AbilityTreeData.Branch)
                    .ThenBy(x => x.AbilityTreeData.Position)
                    .ToArray();
                    
            
            foreach (var ability in allAbilities)
            {
                var abilityModel = new AbilityModel()
                {
                    AbilityId = ability.Identifier.Id,
                    Price = ability.AbilityTreeData.Price,
                    AbilityTreeData = ability.AbilityTreeData,
                    Icon = ability.Icon,
                    Description = ability.Description
                };

                if (abilityModel.Price == 0)
                {
                    abilityModel.IsUnlocked = true;
                }
                

                _abilities.Add(abilityModel);
            }
        }

        public int GetEquippedSlotIndex(AbilityModel slot)
        {
            return _equippedAbilities.FindIndex(0,
                _equippedAbilities.Count, x => x.AbilityId == slot.AbilityId) + 1;
        }

        public AbilityModel GetSlotByIndex(int index)
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
            
            if (ability.AbilityTreeData.UnlockRequirements.All(x => x.CheckRequirement(_gameData.PlayerData))) 
            {
                Souls.Value -= ability.Price;
                ability.IsUnlocked = true;
                _gameData.PlayerData.AbilityData.UnlockedAbilities.Add(ability);
            };
            
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

        public List<AbilityModel> GetSlots()
        {
            return _abilities;
        }

        public int GetResourceCount()
        {
            return _gameData.PlayerData.WorldData.LootData.Collected;
        }
        
        

        public void LoadData()
        {
            if (_gameData.PlayerData.AbilityData.Abilities.Count <= 0) return;

            for (var index = 0; index < _abilities.Count; index++)
            {
                var abilitySlot = _abilities[index];
                abilitySlot.IsEquipped = _gameData.PlayerData.AbilityData.Abilities[index].IsEquipped;
                abilitySlot.IsUnlocked = _gameData.PlayerData.AbilityData.Abilities[index].IsUnlocked;
            }

            foreach (var slot in _gameData.PlayerData.AbilityData.EquippedAbilities)
            {
                var ability = _abilities.FirstOrDefault(x => x.AbilityId == slot.AbilityId);
                _equippedAbilities.Enqueue(ability);
            }
            
        }

        public void SaveModelData()
        {
            _gameData.PlayerData.AbilityData.Abilities = _abilities;
            _gameData.PlayerData.AbilityData.EquippedAbilities = _equippedAbilities;
            _gameData.PlayerData.WorldData.LootData.Collected = Souls.Value;
        }
    }
}
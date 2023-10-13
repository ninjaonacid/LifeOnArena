using System.Collections.Generic;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;

namespace Code.UI.Model.AbilityMenu
{
    public class AbilityMenuModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        private readonly IConfigProvider _configProvider;
        public List<UIAbilitySlotModel> _abilitySlots;

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
                var abilitySlotModel = new UIAbilitySlotModel(ability.Identifier.Name);
                _abilitySlots.Add(abilitySlotModel);
            }

            if (_gameData.PlayerData.SkillSlotsData.AbilitySlots.Count > 0)
            {
                for (var index = 0; index < _abilitySlots.Count; index++)
                {
                    var abilitySlot = _abilitySlots[index];
                    abilitySlot.IsEquipped = _gameData.PlayerData.SkillSlotsData.AbilitySlots[index].IsEquipped;
                }
            }
        }
    }
}

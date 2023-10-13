using System.Collections.Generic;
using Code.Services.PersistentProgress;

namespace Code.UI.Model.AbilityMenu
{
    public class AbilityMenuModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        public List<AbilitySlotModel> _abilitySlots = new();

        public AbilityMenuModel(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }

        public void Initialize()
        {
       
        }
    }
}

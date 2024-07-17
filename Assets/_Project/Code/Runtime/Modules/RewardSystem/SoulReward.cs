using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.UI;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;
using UnityEngine;

namespace Code.Runtime.Modules.RewardSystem
{
    public class SoulReward : IReward
    {
        public bool IsOneTimeReward { get; set; }
        public RewardIdentifier RewardIdentifier { get; set; }
        public LootView LootView { get; set; }


        private readonly ScreenService _screenService;
        private readonly PlayerData _playerData;
        private readonly int _value;
        private readonly Sprite _icon;
        
        public SoulReward(ScreenService screenService, PlayerData playerData, int value,  Sprite icon)
        {
            _screenService = screenService;
            _playerData = playerData;
            _value = value;
            _icon = icon;
        }


        public void Claim()
        {
            _playerData.WorldData.LootData.Collect(_value);
            _screenService.Open(ScreenID.RewardPopup, new SoulRewardDto(_value, _icon));
        }
    }
}

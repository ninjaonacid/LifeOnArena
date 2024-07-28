using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.Audio;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.UI;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;

namespace Code.Runtime.Modules.RewardSystem
{
    public class WeaponReward : IReward
    {
        private readonly WeaponData _weapon;
        private readonly PlayerData _playerData;
        private readonly ScreenService _screenService;
        private readonly AudioService _audioService;

        public WeaponReward(WeaponData weapon, PlayerData playerData, ScreenService screenService, AudioService audioService)
        {
            _weapon = weapon;
            _playerData = playerData;
            _screenService = screenService;
            _audioService = audioService;
        }

        public bool IsOneTimeReward { get; set; }
        public RewardIdentifier RewardIdentifier { get; set; }
        public LootView LootView { get; set; }

        public void Claim()
        {
            if (IsOneTimeReward)
            {
                if (!_playerData.RewardsData.ClaimedRewards.Contains(RewardIdentifier.Id))
                {
                    _playerData.RewardsData.ClaimedRewards.Add(RewardIdentifier.Id);
                };
            }
            _playerData.WorldData.WeaponUnlockedData.UnlockedWeapons.Add(_weapon.WeaponId.Id);
            _audioService.PlaySound("Reward");
            _screenService.Open(ScreenID.RewardPopup, new WeaponRewardDto(_weapon));
        }
    }
}
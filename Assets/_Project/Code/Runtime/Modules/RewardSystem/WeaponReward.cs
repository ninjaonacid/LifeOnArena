using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Modules.RewardSystem
{
    public class WeaponReward : IReward
    {
        private WeaponData _weapon;
        private PlayerData _playerData;


        public WeaponReward(WeaponData weapon, PlayerData playerData)
        {
            _weapon = weapon;
            _playerData = playerData;
        }

        public void Claim()
        {
            _playerData.WorldData.WeaponUnlockedData.UnlockedWeapons.Add(_weapon.WeaponId.Id);
        }
    }
}
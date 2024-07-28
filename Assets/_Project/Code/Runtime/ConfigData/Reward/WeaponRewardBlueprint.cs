using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Modules.RewardSystem;
using UnityEngine;

namespace Code.Runtime.ConfigData.Reward
{
    [CreateAssetMenu(menuName = "Config/Rewards/WeaponReward", fileName = "WeaponReward")]
    public class WeaponRewardBlueprint : AbstractRewardBlueprint<WeaponReward>
    {
        [SerializeField] private WeaponData _weapon;
        
        public override IReward GetReward()
        {
            return new WeaponReward(_weapon, _playerData, _screenService, _audioService);
        }
    }
}
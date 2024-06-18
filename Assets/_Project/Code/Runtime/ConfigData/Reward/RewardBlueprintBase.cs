using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.ConfigData.Reward
{
    public abstract class RewardBlueprintBase : ScriptableObject
    {
        protected PlayerData _playerData;
        
        public void InjectServices(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}

using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    public class SoulsRequirement : IRequirement<PlayerData>
    {
        [SerializeField] private int _requiredSouls;
        
        public bool CheckRequirement(PlayerData value)
        {
            return value.WorldData.LootData.Collected >= _requiredSouls;
        }
    }
}
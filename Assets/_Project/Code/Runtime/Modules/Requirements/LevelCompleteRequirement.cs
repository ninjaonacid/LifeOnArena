using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    public class LevelCompleteRequirement : IRequirement<PlayerData>
    {
        [SerializeField] private LevelIdentifier _levelId;
        public bool CheckRequirement(PlayerData value)
        {
            return value.WorldData.LocationProgressData.CompletedLocations.Contains(_levelId.Id);
        }
    }
}
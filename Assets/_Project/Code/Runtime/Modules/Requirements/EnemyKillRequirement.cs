using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    public class EnemyKillRequirement : IRequirement<PlayerData>
    {
        [SerializeField] private MobIdentifier _enemyToKill;

        public bool CheckRequirement(PlayerData value)
        {
            return value.KillData.KilledBosses.Contains(_enemyToKill.Id);
        }
    }
}

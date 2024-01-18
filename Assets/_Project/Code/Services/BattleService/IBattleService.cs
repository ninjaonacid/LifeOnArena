using Code.ConfigData.StatSystem;
using UnityEngine;

namespace Code.Services.BattleService
{
    public interface IBattleService : IService
    {
        int CreateAttack(StatController attackerStats, Vector3 attackPoint, LayerMask mask);
        void ApplyDamage(StatController attacker, GameObject target);
    }
}

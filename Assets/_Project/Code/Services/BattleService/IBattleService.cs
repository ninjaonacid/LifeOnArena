using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Services.BattleService
{
    public interface IBattleService : IService
    {
        void CreateAttack(StatController attackerStats, Vector3 attackPoint, LayerMask mask);
    }
}

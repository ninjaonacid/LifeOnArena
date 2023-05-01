using UnityEngine;

namespace Code.Services.BattleService
{
    public interface IBattleService : IService
    {
        void AoeAttack(float damage, float radius, int maxTargets, Vector3 worldPoint, LayerMask mask);
    }
}

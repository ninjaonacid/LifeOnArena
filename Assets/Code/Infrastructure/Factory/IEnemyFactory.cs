using Code.Enemy;
using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IEnemyFactory : IService
    {
        void CreateSpawner(Vector3 at, string spawnerDataId, MonsterTypeId spawnerDataMonsterTypeId);
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        LootPiece CreateLoot();
    }
}

using Code.Enemy;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IEnemyFactory : IService
    {
        UniTask<EnemySpawner> CreateSpawner(Vector3 at, string spawnerDataId, MonsterTypeId spawnerDataMonsterTypeId,
            int RespawnCount);
        UniTask<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        UniTask<LootPiece> CreateLoot();
        UniTask InitAssets();
    }
}

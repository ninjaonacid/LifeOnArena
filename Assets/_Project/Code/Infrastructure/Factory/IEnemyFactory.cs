using System.Threading;
using Code.Entity.Enemy;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.StaticData;
using Code.StaticData.Identifiers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IEnemyFactory : IService
    {
        UniTask<EnemySpawner> CreateSpawner(Vector3 at, string spawnerDataId, MonsterTypeId spawnerDataMonsterTypeId,
            int RespawnCount, CancellationToken token);
        UniTask<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent, CancellationToken token);
        UniTask<LootPiece> CreateLoot();
        UniTask InitAssets();
    }
}

using System.Threading.Tasks;
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
        Task<EnemySpawnPoint> CreateSpawner(Vector3 at, string spawnerDataId, MonsterTypeId spawnerDataMonsterTypeId, int RespawnCount);
        Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        Task<LootPiece> CreateLoot();
        UniTask InitAssets();
    }
}

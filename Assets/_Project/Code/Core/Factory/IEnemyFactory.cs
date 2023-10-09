using System.Threading;
using Code.ConfigData.Identifiers;
using Code.Entity.Enemy;
using Code.Logic.EnemySpawners;
using Code.Logic.Particles;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Core.Factory
{
    public interface IEnemyFactory : IService
    {
        UniTask<EnemySpawner> CreateSpawner(Vector3 at, string spawnerDataId, MobIdentifier MobId,
            int RespawnCount, CancellationToken token);
        UniTask<GameObject> CreateMonster(MobId mobId, Transform parent, CancellationToken token);
        UniTask<SoulLoot> CreateSoulLoot();
        UniTask InitAssets();
    }
}

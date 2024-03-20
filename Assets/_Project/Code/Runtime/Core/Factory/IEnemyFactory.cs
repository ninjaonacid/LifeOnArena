using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Logic.EnemySpawners;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Core.Factory
{
    public interface IEnemyFactory
    {
        UniTask<EnemySpawner> CreateSpawner(Vector3 at, string spawnerDataId, MobIdentifier mobId,
            int respawnCount, EnemyType enemyType, CancellationToken token);
        UniTask<GameObject> CreateMonster(int mobId, Transform parent, CancellationToken token);
        UniTask InitAssets();
    }
}

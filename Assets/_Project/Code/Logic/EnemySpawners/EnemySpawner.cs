using System.Threading;
using Code.Data;
using Code.Entity.Enemy;
using Code.Infrastructure.ObjectPool;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.StaticData;
using Code.StaticData.Identifiers;
using Code.StaticData.StatSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawner : MonoBehaviour, ISave
    {
        private EnemyDeath _enemyDeath;
        private IEnemyObjectPool _enemyObjectPool;
        private IParticleObjectPool _particleObjectPool;
        private ILevelEventHandler _levelEventHandler;

        private float _enemyTimer = 15;

        private GameObject _spawnParticle;
        public string Id { get; set; }
        public int RespawnCount { get; set; }

        public MonsterTypeId MonsterTypeId;
        public AssetReference SpawnParticle;
        public bool Alive  { get; private set; }

        [Inject]
        public void Construct(IEnemyObjectPool enemyObjectPool,
            IParticleObjectPool particleObjectPool,
            ILevelEventHandler levelEventHandler
        )
        {
            _enemyObjectPool = enemyObjectPool;
            _particleObjectPool = particleObjectPool;
            _levelEventHandler = levelEventHandler;
        }

        public void LoadData(PlayerData data)
        {
            //if (progress.KillData.ClearedSpawners.Contains(Id))
            //    Alive = true;
            //else
            //    Spawn();

            //StartCoroutine(ChangeSpawnTimer());
        }

        public void UpdateData(PlayerData data)
        {
            if (!Alive)
                data.KillData.ClearedSpawners.Add(Id);
        }

        public async UniTaskVoid Spawn(CancellationToken token)
        {
            Alive = true;
            var monster = await _enemyObjectPool.GetObject(MonsterTypeId, transform, token);
            _spawnParticle = await _particleObjectPool.GetObject(SpawnParticle, transform);
            
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                  _enemyDeath.Happened -= Slay;

            _enemyObjectPool.ReturnObject(MonsterTypeId, _enemyDeath.gameObject);
            _particleObjectPool.ReturnObject(SpawnParticle, _spawnParticle);

            Alive = false;
        }

    }
}
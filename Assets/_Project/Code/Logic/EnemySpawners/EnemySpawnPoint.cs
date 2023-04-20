using System.Collections;
using Code.Data;
using Code.Enemy;
using Code.Infrastructure.ObjectPool;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.StaticData;
using UnityEngine;
using VContainer;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawnPoint : MonoBehaviour, ISave
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
        public ParticleId ParticleId = ParticleId.SpawnParticle;
        public bool Slain { get; private set; }

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

        public void LoadProgress(PlayerProgress progress)
        {
            //if (progress.KillData.ClearedSpawners.Contains(Id))
            //    Slain = true;
            //else
            //    Spawn();

            //StartCoroutine(ChangeSpawnTimer());
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (Slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        public async void Spawn()
        {
            var monster = await _enemyObjectPool.GetObject(MonsterTypeId, transform);
            _spawnParticle = _particleObjectPool.GetObject(ParticleId, transform);

            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {

            /* if (_enemyDeath != null)
                  _enemyDeath.Happened -= Slay;*/

            _enemyObjectPool.ReturnObject(MonsterTypeId, _enemyDeath.gameObject);
            _particleObjectPool.ReturnObject(ParticleId, _spawnParticle);

            if (RespawnCount > 0)
                StartCoroutine(RespawnEnemy());

            else
            {
                Slain = true;
                _levelEventHandler.MonsterSpawnerSlain(this);
            }
        }

        private IEnumerator RespawnEnemy()
        {
            RespawnCount--;
            yield return new WaitForSeconds(3);

            _enemyObjectPool.GetObject(MonsterTypeId, transform);
            _particleObjectPool.GetObject(ParticleId, transform);
        }

        private IEnumerator ChangeSpawnTimer()
        {
            while (_enemyTimer > 0)
            {
                _enemyTimer -= Time.deltaTime; 
                yield return null;
            }
            _enemyTimer = 30;
            yield break;
        }
    }
}
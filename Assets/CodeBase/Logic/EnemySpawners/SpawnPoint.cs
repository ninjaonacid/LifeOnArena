using System.Collections;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.ObjectPool;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        private EnemyDeath _enemyDeath;
        private IObjectPool _enemyObjectPool;

        private float _enemyTimer = 15;
        public string Id { get; set; }

        public MonsterTypeId MonsterTypeId;

        public bool Slain { get; private set; }

        public void Construct()
        {
            _enemyObjectPool = AllServices.Container.Single<IObjectPool>();

        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                Slain = true;
            else
                Spawn();

            StartCoroutine(ChangeSpawnTimer());
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (Slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        private void Spawn()
        {
            var monster = _enemyObjectPool.GetObject(MonsterTypeId, transform);

            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
           /* if (_enemyDeath != null)
                 _enemyDeath.Happened -= Slay;*/

            _enemyObjectPool.ReturnObject(MonsterTypeId, _enemyDeath.gameObject);

            StartCoroutine(RespawnEnemy());
            Slain = true;

        }

        private IEnumerator RespawnEnemy()
        {
            yield return new WaitForSeconds(3);

            _enemyObjectPool.GetObject(MonsterTypeId, transform);
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
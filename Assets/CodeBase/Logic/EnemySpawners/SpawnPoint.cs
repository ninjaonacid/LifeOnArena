using System.Collections;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.ObjectPool;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        private EnemyDeath _enemyDeath;
        private IObjectPool _enemyObjectPool;
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

            _enemyObjectPool.ReturnObject(_enemyDeath.gameObject);
            StartCoroutine(RespawnEnemy());
            Slain = true;

        }

        private IEnumerator RespawnEnemy()
        {
            yield return new WaitForSeconds(3);
            var monster = _enemyObjectPool.GetObject(MonsterTypeId, transform);
            monster.transform.position = transform.position;
            monster.SetActive(true);

        }
    }
}
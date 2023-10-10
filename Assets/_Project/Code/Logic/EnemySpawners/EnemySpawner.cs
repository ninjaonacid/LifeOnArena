using System.Threading;
using Code.ConfigData.Identifiers;
using Code.Core.ObjectPool;
using Code.Entity.Enemy;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyDeath _enemyDeath;
        private EnemyObjectPool _enemyObjectPool;
        private ViewObjectPool _viewObjectPool;
        
        private GameObject _spawnParticle;
        public string Id { get; set; }
        public int RespawnCount { get; set; }

        public MobIdentifier MobId;
        public Identifier ParticleIdentifier;
        public bool Alive  { get; private set; }

        [Inject]
        public void Construct(EnemyObjectPool enemyObjectPool,
            ViewObjectPool viewObjectPool)
        {
            _enemyObjectPool = enemyObjectPool;
            _viewObjectPool = viewObjectPool;
        }

        public async UniTaskVoid Spawn(CancellationToken token)
        {
            Alive = true;
            var monster = await _enemyObjectPool.GetObject(MobId.Id, transform, token);
           _spawnParticle = await _viewObjectPool.GetObject(ParticleIdentifier.Id, transform);
            
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                  _enemyDeath.Happened -= Slay;

            _enemyObjectPool.ReturnObject(MobId.Id, _enemyDeath.gameObject);
            _viewObjectPool.ReturnObject(ParticleIdentifier.Id, _spawnParticle);

            Alive = false;
        }

    }
}
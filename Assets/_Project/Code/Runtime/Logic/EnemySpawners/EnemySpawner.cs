using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Entity.Enemy;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Logic.EnemySpawners
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyDeath _enemyDeath;
        private IPoolable _pooledObject;
        private IEnemyFactory _factory;
        private ParticleFactory _particleFactory;
        private ParticleSystem _spawnParticle;
        public string Id { get; set; }
        public int RespawnCount { get; set; }

        public MobIdentifier MobId;
        public Identifier ParticleIdentifier;
        public bool Alive  { get; private set; }

        [Inject]
        public void Construct(IEnemyFactory enemyFactory,
            ParticleFactory particleFactory)
        {
            _factory = enemyFactory;
            _particleFactory = particleFactory;
        }

        public async UniTaskVoid Spawn(CancellationToken token)
        {
            Alive = true;
            var monster = await _factory.CreateMonster(MobId.Id, transform, token);

            var position = transform.position;
            monster.transform.position = position;

            _spawnParticle = await _particleFactory.CreateParticleWithTimer(ParticleIdentifier.Id, position, 2);
            
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                  _enemyDeath.Happened -= Slay;

            Alive = false;
        }

    }
}
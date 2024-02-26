using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Entity.Enemy;
using Code.Runtime.Logic.VisualEffects;
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
        private VisualEffectFactory _visualEffectFactory;
        private VisualEffect _spawnVfx;
        public string Id { get; set; }
        public int RespawnCount { get; set; }

        public MobIdentifier MobId;
        public Identifier ParticleIdentifier;
        public bool Alive  { get; private set; }

        [Inject]
        public void Construct(IEnemyFactory enemyFactory,
            VisualEffectFactory visualEffectFactory)
        {
            _factory = enemyFactory;
            _visualEffectFactory = visualEffectFactory;
        }

        public async UniTaskVoid Spawn(CancellationToken token)
        {
            Alive = true;
            var monster = await _factory.CreateMonster(MobId.Id, transform, token);

            var position = transform.position;
            monster.transform.position = position;

            _spawnVfx = await _visualEffectFactory.CreateVisualEffectWithTimer(ParticleIdentifier.Id, position, 2);
            
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
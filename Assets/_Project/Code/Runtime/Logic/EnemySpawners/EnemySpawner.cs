using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Levels;
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
        [SerializeField] private VisualEffectIdentifier _visualEffectIdentifier;
        public string Id { get; set; }
        public int RespawnCount { get; set; }
        public float TimeToSpawn { get; set; }
        public EnemyType EnemyType { get; set; }
        public MobIdentifier MobId { get; set; }
        public bool Alive  { get; private set; }
        
        private EnemyDeath _enemyDeath;
        private IPoolable _pooledObject;
        private IEnemyFactory _factory;
        private VisualEffectFactory _visualEffectFactory;
        private VisualEffect _spawnVfx;

        [Inject]
        public void Construct(IEnemyFactory enemyFactory,
            VisualEffectFactory visualEffectFactory)
        {
            _factory = enemyFactory;
            _visualEffectFactory = visualEffectFactory;
        }

        public void InitializeSpawner(LevelConfig levelConfig)
        {
            _visualEffectFactory.PrewarmEffect(_visualEffectIdentifier.Id, 1).Forget();
        }

        public async UniTask Spawn(CancellationToken token)
        {
            Alive = true;
            if (TimeToSpawn > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(TimeToSpawn), cancellationToken: token);
            }
            
            var position = transform.position;
            await VfxSpawnLogic(position, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: token);
            
            var monster = await _factory.CreateMonster(MobId.Id, transform, token);
            
            
            monster.transform.position = position;
            monster.GetComponent<NavMeshMoveToPlayer>().Warp(position);

            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private async Task VfxSpawnLogic(Vector3 position, CancellationToken token)
        {
            _spawnVfx = await _visualEffectFactory.CreateVisualEffect(_visualEffectIdentifier.Id, position, token);

            var spawnVfxPosition = _spawnVfx.transform.position;
            spawnVfxPosition =
                new Vector3(spawnVfxPosition.x, spawnVfxPosition.y + 5, spawnVfxPosition.z);

            _spawnVfx.transform.position = spawnVfxPosition;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                  _enemyDeath.Happened -= Slay;

            Alive = false;
        }

    }
}
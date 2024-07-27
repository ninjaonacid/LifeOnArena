using System;
using System.Threading;
using System.Threading.Tasks;
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
        public event Action<EnemySpawner> SpawnerCleared;
        
        [SerializeField] private VisualEffectIdentifier _visualEffectIdentifier;
        public string Id { get; set; }
        public int RespawnCount { get; set; }
        public float TimeToSpawn { get; set; }
        public EnemyType EnemyType { get; set; }
        public MobIdentifier MobId { get; set; }
        public bool Alive  { get; private set; }
        
        private EnemyDeath _enemyDeath;
        private EnemyFactory _factory;
        private VisualEffectFactory _visualEffectFactory;
        private VisualEffect _spawnVfx;
        private int _spawnCounter;

        [Inject]
        public void Construct(EnemyFactory enemyFactory,
            VisualEffectFactory visualEffectFactory)
        {
            _factory = enemyFactory;
            _visualEffectFactory = visualEffectFactory;
        }

        public void InitializeSpawner()
        {
            _visualEffectFactory.PrewarmEffect(_visualEffectIdentifier.Id, 1).Forget();
        }

        public async UniTask<GameObject> Spawn(CancellationToken token)
        {
            if (_spawnCounter >= RespawnCount) return null;
            
            Alive = true;
            
            if (TimeToSpawn > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(TimeToSpawn), cancellationToken: token);
            }
            
            var position = transform.position;
            
            await VfxSpawnLogic(position, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: token);
            
            var monster = await _factory.CreateMonster(MobId.Id, transform, token, onCreate: OnSpawn);
            
            monster.GetComponent<NavMeshMoveToPlayer>().Warp(position);
            
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
            _spawnCounter++;
            
            return monster;
        }

        private async Task VfxSpawnLogic(Vector3 position, CancellationToken token)
        {
            _spawnVfx = await _visualEffectFactory.CreateVisualEffect(_visualEffectIdentifier.Id, position, token);

            var spawnVfxTransform = _spawnVfx.transform;

            var spawnVfxPosition = spawnVfxTransform.position;
            spawnVfxPosition =
                new Vector3(spawnVfxPosition.x, spawnVfxPosition.y + 5, spawnVfxPosition.z);

            spawnVfxTransform.position = spawnVfxPosition;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                  _enemyDeath.Happened -= Slay;
            
            Alive = false;
            SpawnerCleared?.Invoke(this);
        }

        private void OnSpawn(PooledObject monster)
        {
        }

    }
}
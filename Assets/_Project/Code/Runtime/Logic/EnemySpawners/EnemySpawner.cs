using System.Threading;
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
            _visualEffectFactory.PrewarmEffect(_visualEffectIdentifier.Id, levelConfig.EnemySpawners.Count).Forget();
        }

        public async UniTaskVoid Spawn(CancellationToken token)
        {
            Alive = true;
            var monster = await _factory.CreateMonster(MobId.Id, transform, token);

            var position = transform.position;
            monster.transform.position = position;

            _spawnVfx = await _visualEffectFactory.CreateVisualEffect(_visualEffectIdentifier.Id, position);
            var spawnVfxPosition = _spawnVfx.transform.position;
            spawnVfxPosition =
                new Vector3(spawnVfxPosition.x, spawnVfxPosition.y + 2, spawnVfxPosition.z);
            
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
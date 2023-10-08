using Code.Core.Factory;
using Code.Data;
using Code.Logic.Particles;
using Code.Services.RandomService;
using UnityEngine;

namespace Code.Entity.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        private IRandomService _randomService;
        private IEnemyFactory _enemyFactory;
        public EnemyDeath EnemyDeath;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IEnemyFactory factory, IRandomService randomService)
        {
            _enemyFactory = factory;
            _randomService = randomService;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        private async void SpawnLoot()
        {
            SoulParticle loot = await _enemyFactory.CreateLoot();

            loot.transform.position = transform.position;

            var lootItem = new Loot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };

            //loot.Init(lootItem);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}
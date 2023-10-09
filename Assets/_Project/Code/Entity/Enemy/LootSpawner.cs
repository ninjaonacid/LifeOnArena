using Code.Core.Factory;
using Code.Data;
using Code.Data.PlayerData;
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
            SoulLoot loot = await _enemyFactory.CreateExp();

            loot.transform.position = transform.position + new Vector3(0, 2, 0);

            var lootItem = new Loot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };
            
            loot.Initialize(lootItem);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.RandomService;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        private IRandomService _randomService;
        private IEnemyFactory _factory;
        public EnemyDeath EnemyDeath;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IEnemyFactory factory, IRandomService randomService)
        {
            _factory = factory;
            _randomService = randomService;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();

            loot.transform.position = transform.position;

            var lootItem = new Loot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };

            loot.Init(lootItem);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}
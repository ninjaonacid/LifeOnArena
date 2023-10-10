using Code.Core.Factory;
using Code.Core.ObjectPool;
using Code.Data.PlayerData;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using UnityEngine;

namespace Code.Entity.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        private IRandomService _randomService;
        private IItemFactory _itemFactory;
        private IGameDataContainer _dataContainer;
        private ViewObjectPool _viewObjectPool;
        public EnemyDeath EnemyDeath;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IItemFactory factory, IRandomService randomService, IGameDataContainer dataContainer, ViewObjectPool viewObjectPool)
        {
            _itemFactory = factory;
            _randomService = randomService;
            _dataContainer = dataContainer;
            _viewObjectPool = viewObjectPool;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        private async void SpawnLoot()
        {
            GameObject loot = await _viewObjectPool.GetObject();

            loot.transform.position = transform.position + new Vector3(0, 2, 0);

            var lootItem = new Loot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };
            
            _dataContainer.PlayerData.WorldData.LootData.Collect(lootItem);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}
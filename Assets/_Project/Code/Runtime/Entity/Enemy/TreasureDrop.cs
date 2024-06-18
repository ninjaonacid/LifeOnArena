using Code.Runtime.Core.Factory;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class TreasureDrop : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private ItemFactory _itemFactory;
        
        
        public void Construct(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _enemyDeath.Happened += SpawnTreasure;
        }

        public void SpawnTreasure()
        {
            
        }
    }
}

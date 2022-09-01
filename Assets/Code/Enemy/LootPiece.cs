using Code.Data;
using UnityEngine;

namespace Code.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject LootPrefab;
        

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Init(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) =>
            Pickup();
        
        

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;

            _worldData.LootData.Collect(_loot);

            Destroy(this.gameObject);

        }
    }
}

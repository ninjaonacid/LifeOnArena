using Code.Data;
using Code.Data.PlayerData;
using TMPro;
using UnityEngine;

namespace Code.UI.View.HUD
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;

        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.CountChanged += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        public void UpdateCounter()
        {
            Counter.text = $"{_worldData.LootData.Collected}";
        }

    }
}

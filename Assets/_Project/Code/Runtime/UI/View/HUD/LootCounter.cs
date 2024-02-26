using Code.Runtime.Data.PlayerData;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.View.HUD
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;

        private WorldData _worldData;
        private Tween _counterTween;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.CountChanged += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter(int value)
        {
            int collected = _worldData.LootData.Collected - value;

            _counterTween = DOTween
                .To(() => collected, x => collected = x, collected + value, 1)
                .OnUpdate(() =>
                {
                    Counter.text = $"{collected}";
                });
            
        }

        private void UpdateCounter()
        {
            Counter.text = $"{_worldData.LootData.Collected}";
        }

    }
}

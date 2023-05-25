using Code.Data;
using Code.Services.PersistentProgress;
using Code.StaticData.StatSystem;
using UnityEngine;
using Attribute = Code.StaticData.StatSystem.Attribute;

namespace Code.Hero
{
    public class HeroStats : StatController, ISave
    {
        private CharacterStatsData _characterStatsData;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Attribute health = _stats["Health"] as Attribute;
                health.ApplyModifier(new StatModifier()
                {
                    OperationType = ModifierOperationType.Additive,
                    Magnitude = -10,
                    Source = this,
                });
                Debug.Log(health.BaseValue.ToString());
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _characterStatsData = progress.CharacterStatsData;

            foreach (var stat in Stats.Values)
            {
                if (stat is ISave saveable)
                {
                    saveable.LoadProgress(progress);
                }
            }
            
            StatsLoaded();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            foreach (var stat in Stats.Values)
            {
                if (stat is ISave saveable)
                {
                    saveable.UpdateProgress(progress);
                }
            }
        }
    }
}

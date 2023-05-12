using System;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Hero
{
    public class HeroStats : StatController, ISave
    {
        private CharacterStatsData _characterStatsData;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _stats["Attack"].AddModifier( new StatModifier
                {
                    Magnitude = 20,
                    OperationType = ModifierOperationType.Additive
                });

                Debug.Log(_stats["Attack"].Value.ToString());
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
                    stat.Initialize();
                }
            }
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

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
        public void LoadProgress(PlayerProgress progress)
        {
            _characterStatsData = progress.CharacterStatsData;

            foreach (var statData in _characterStatsData.StatsData)
            {
               Stat data = Stats[statData.Key];
               data.Initialize(statData.Value);
            }
        }

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

        public void UpdateProgress(PlayerProgress progress)
        {
            foreach (var stat in Stats)
            {
                if (!_characterStatsData.StatsData.ContainsKey(stat.Key))
                {
                    _characterStatsData.StatsData.Add(stat.Key, stat.Value.Value);
                }
                else
                {
                    _characterStatsData.StatsData[stat.Key] = stat.Value.Value;
                }
                
            }
        }
    }
}

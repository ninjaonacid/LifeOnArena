using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.StaticData.StatSystem
{
    public class PrimaryStat : Stat, ISave
    {
        private int _baseValue;
        public override int BaseValue => _baseValue;

        public PrimaryStat(StatDefinition statDefinition, StatController statController) : base(statDefinition, statController)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            _baseValue = _statDefinition.BaseValue;
        }

        public void Add(int amount)
        {
            _baseValue += amount;
            CalculateValue();
        }

        public void Substract(int amount)
        {
            _baseValue -= amount;
            CalculateValue();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.CharacterStatsData.StatsData.TryGetValue(_statDefinition.name, out var value))
            {
                _baseValue = value;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.CharacterStatsData.StatsData.TryAdd(_statDefinition.name, _baseValue))
            {
                
            }
            else
            {
                progress.CharacterStatsData.StatsData[_statDefinition.name] = _baseValue;
            }
        }
    }
}

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
            _baseValue = _statDefinition.BaseValue;
            base.Initialize();
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

        public void LoadData(PlayerData data)
        {
            if (data.StatsData.Stats.TryGetValue(_statDefinition.name, out var value))
            {
                _baseValue = value;
            }
            
            CalculateValue();
        }

        public void UpdateData(PlayerData data)
        {
            if (data.StatsData.Stats.TryAdd(_statDefinition.name, _baseValue))
            {
                
            }
            else
            {
                data.StatsData.Stats[_statDefinition.name] = _baseValue;
            }
        }
    }
}

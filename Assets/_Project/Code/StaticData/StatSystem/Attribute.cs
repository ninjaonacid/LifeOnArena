using System;
using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.StaticData.StatSystem
{
    public class Attribute : Stat, ISave
    {
        protected int _currentValue;
        public int CurrentValue => _currentValue;
        public event Action CurrentValueChanged;
        public event Action<StatModifier> AppliedModifier;

        public Attribute(StatDefinition statDefinition, StatController statController) : base(statDefinition, statController)
        {
        }
        
        public override void Initialize()
        {
            base.Initialize();
            _currentValue = Value;
        }

        public virtual void ApplyModifier(StatModifier modifier)
        {
            int newValue = _currentValue;

            switch (modifier.OperationType)
            {
                case ModifierOperationType.Override:
                    newValue = modifier.Magnitude;
                    break;

                case ModifierOperationType.Additive:
                    newValue += modifier.Magnitude;
                    break;

                case ModifierOperationType.Multiplicative:
                    newValue *= modifier.Magnitude;
                    break;
            }

            newValue = Mathf.Clamp(newValue, 0, _currentValue);

            if (_currentValue != newValue)
            {
                _currentValue = newValue;
                CurrentValueChanged?.Invoke();
                AppliedModifier?.Invoke(modifier);
            }
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.CharacterStatsData.StatsData.TryGetValue(_statDefinition.name, out var value))
            {
                _currentValue = value;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.CharacterStatsData.StatsData.TryAdd(_statDefinition.name, _currentValue))
            {
                
            }
            else
            {
                progress.CharacterStatsData.StatsData[_statDefinition.name] = _currentValue;
            }
        }
        
    }
}

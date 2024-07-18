using System;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Modules.StatSystem
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

        public void Add(int value)
        {
            _value += value;
            _currentValue = _value;
            CurrentValueChanged?.Invoke();
        }

        protected void AttributeValueChanged()
        {
            CurrentValueChanged?.Invoke();
        }
        
        public  void LoadData(PlayerData data)
        {
            if (data.StatsData.StatsValues.TryGetValue(_statDefinition.name, out var value))
            {
                _currentValue = value;
                CurrentValueChanged?.Invoke();
            }
        }

        public void UpdateData(PlayerData data)
        {
            if (data.StatsData.StatsValues.TryAdd(_statDefinition.name, _currentValue))
            {
                
            }
            else
            {
                data.StatsData.StatsValues[_statDefinition.name] = _currentValue;
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.StaticData.StatSystem
{
    public class Stat : ISave
    {
        protected StatDefinition _statDefinition;
        protected StatController _statController;
        protected int _value;
        public int Value => _value;
        public virtual int BaseValue
        {
            get => _statDefinition.BaseValue;
            private set => _value = value;
        }

        public event Action ValueChanged;
        protected List<StatModifier> _modifiers = new List<StatModifier>();
        
        public Stat(StatDefinition statDefinition, StatController statController)
        {
            _statDefinition = statDefinition;
            _statController = statController;
        }

        public virtual void Initialize()
        {
            CalculateValue();
        }
        
        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            CalculateValue();
        }

        public void RemoveModifier(Object source)
        {
            _modifiers = _modifiers.Where(x => x.Source.GetInstanceID() != source.GetInstanceID()).ToList();
            CalculateValue();
        }

        protected void CalculateValue()
        {
            int newValue = BaseValue;

            _modifiers.Sort((x, y) => x.OperationType.CompareTo(y.OperationType));

            foreach (var modifier in _modifiers)
            {
                if (modifier.OperationType == ModifierOperationType.Additive)
                {
                    newValue += modifier.Magnitude;
                }

                else if (modifier.OperationType == ModifierOperationType.Multiplicative)
                {
                    newValue *= modifier.Magnitude;
                }
            }

            if (_statDefinition.Capacity >= 0)
            {
                newValue = Mathf.Min(newValue, _statDefinition.Capacity);
            }

            if (_value != newValue)
            {
                _value = newValue;
                ValueChanged?.Invoke();
            }
        }


        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.CharacterStatsData.StatsData.TryGetValue(_statDefinition.name, out var value))
            {
                BaseValue = value;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.CharacterStatsData.StatsData.TryAdd(_statDefinition.name, _value))
            {
                
            }
            else
            {
                progress.CharacterStatsData.StatsData[_statDefinition.name] = _value;
            }
        }
    }
}

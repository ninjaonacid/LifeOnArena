using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Runtime.Modules.StatSystem.Runtime
{
    public class Stat
    {
        protected StatDefinition _statDefinition;
        protected StatController _statController;
        protected int _value;
        public int Value => _value;
        public virtual int BaseValue => _statDefinition.BaseValue;
        public int StatPerLevel => _statDefinition.StatPerLevel;
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
            int num = _modifiers.RemoveAll(x => (Object)x.Source == source);

            if (num > 0)
            {
                CalculateValue();
            }
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
        
        
    }
}

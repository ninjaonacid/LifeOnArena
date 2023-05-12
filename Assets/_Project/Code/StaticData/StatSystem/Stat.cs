using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.StaticData.StatSystem
{
    public class Stat
    {
        protected StatDefinition _statDefinition;
        protected StatController _statController;
        protected int _value;
        public int Value => _value;
        public virtual int BaseValue => _statDefinition.BaseValue;
        public event Action ValueChanged;
        protected List<StatModifier> _modifiers = new List<StatModifier>();
        
        public Stat(StatDefinition statDefinition, StatController statController)
        {
            _statDefinition = statDefinition;
            _statController = statController;
        }

        public virtual void Initialize(int value)
        {
            _value = value;
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
    }
}

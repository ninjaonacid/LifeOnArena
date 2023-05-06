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
        protected int _value;
        public int Value => _value;
        public virtual int BaseValue => _statDefinition.BaseValue;
        public event Action ValueChanged;
        protected List<StatModifier> _modifiers = new List<StatModifier>();


        public Stat(StatDefinition statDefinition)
        {
            
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(Object source)
        {
            _modifiers = _modifiers.Where(x => x.Source.GetInstanceID() != source.GetInstanceID()).ToList();
        }

        protected void CalculateValue()
        {
            int finalValue = BaseValue;

            _modifiers.Sort();

            _modifiers.Sort((x, y) => x.OperationType.CompareTo(y.OperationType));

            foreach (var modifier in _modifiers)
            {
                if (modifier.OperationType == ModifierOperationType.Additive)
                {
                    finalValue += modifier.Magnitude;
                }

                else if (modifier.OperationType == ModifierOperationType.Multiplicative)
                {
                    finalValue *= modifier.Magnitude;
                }
            }

            if (_statDefinition.Capacity >= 0)
            {
                finalValue = Mathf.Min(finalValue, _statDefinition.Capacity);
            }

            if (_value != finalValue)
            {
                _value = finalValue;
                ValueChanged?.Invoke();
            }
        }
    }
}

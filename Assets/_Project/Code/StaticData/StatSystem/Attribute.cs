using System;
using UnityEngine;

namespace Code.StaticData.StatSystem
{
    public class Attribute : Stat
    {
        protected int _currentValue;
        public int CurrentValue => _currentValue;
        public event Action CurrentValueChanged;

        public event Action<StatModifier> AppliedModifier;

        public Attribute(StatDefinition statDefinition, StatController statController) : base(statDefinition, statController)
        {
        }

        public void Initialize()
        {
            _currentValue = BaseValue;
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
    }
}

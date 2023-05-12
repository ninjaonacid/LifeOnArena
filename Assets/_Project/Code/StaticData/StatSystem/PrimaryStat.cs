using UnityEngine;

namespace Code.StaticData.StatSystem
{
    public class PrimaryStat : Stat
    {
        private int _baseValue;
        public override int BaseValue => _baseValue;

        public PrimaryStat(StatDefinition statDefinition, StatController statController) : base(statDefinition, statController)
        {
        }

        public override void Initialize(int value)
        {
            base.Initialize(value);
            _baseValue = value;
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
    }
}

using System;
using Code.StaticData.StatSystem;
using Attribute = Code.StaticData.StatSystem.Attribute;

namespace Code.Logic.EntitiesComponents
{
    public interface IHealth
    {
        float Current { get; set; }
        float Max { get; }

        Attribute Health { get; set; }
        event Action HealthChanged;

        void TakeDamage(int damage);
    }
}
using System;
using Code.StaticData.StatSystem;

namespace Code.Logic.EntitiesComponents
{
    public interface IHealth
    {
        float Current { get; set; }
        float Max { get; set; }
        Stat Health { set; }

        event Action HealthChanged;

        void TakeDamage(float damage);
    }
}
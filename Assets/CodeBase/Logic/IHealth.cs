using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        float Current { get; set; }
        float Max { get; set; }

        event Action HealthChanged;

        void TakeDamage(float damage);
    }
}
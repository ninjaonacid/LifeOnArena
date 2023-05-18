using System;
using Code.StaticData.StatSystem;
using Attribute = Code.StaticData.StatSystem.Attribute;

namespace Code.Logic.EntitiesComponents
{
    public interface IHealth
    {
        Attribute Health { get; set; }
        event Action HealthChanged;
        void TakeDamage(int damage);
    }
}
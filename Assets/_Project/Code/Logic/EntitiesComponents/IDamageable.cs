using System;
using Code.ConfigData.StatSystem;
using Code.Logic.Damage;
using Attribute = Code.ConfigData.StatSystem.Attribute;

namespace Code.Logic.EntitiesComponents
{
    public interface IDamageable
    {
        Health Health { get; }
        void TakeDamage(IDamage damage);
    }
}
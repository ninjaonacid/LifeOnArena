using System;
using Code.StaticData.StatSystem;
using Attribute = Code.StaticData.StatSystem.Attribute;

namespace Code.Logic.EntitiesComponents
{
    public interface IDamageable
    {
        Health Health { get; set; }
        void TakeDamage(int damage);
    }
}
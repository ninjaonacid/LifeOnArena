﻿using Code.Runtime.Logic.Damage;
using Code.Runtime.Modules.StatSystem;

namespace Code.Runtime.Entity.EntitiesComponents
{
    public interface IDamageable
    {
        Health Health { get; }
        void TakeDamage(IDamage damage);
    }
}
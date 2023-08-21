using System;

namespace Code.Logic.Damage
{
    public interface IDamage
    {
        DamageType DamageType { get; }
        bool IsCriticalHit { get; }
        int Value { get; }
        IDamageSource Source { get; }

    }
}

using UnityEngine;

namespace Code.Runtime.Logic.Damage
{
    public interface IDamage
    {
        GameObject Attacker { get; }
        bool IsCriticalHit { get; }
        int Magnitude { get; }
        IDamageSource Source { get; }
    }
}

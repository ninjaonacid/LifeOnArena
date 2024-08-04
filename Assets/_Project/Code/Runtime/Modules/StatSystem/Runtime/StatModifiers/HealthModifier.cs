using Code.Runtime.Logic.Damage;
using UnityEngine;

namespace Code.Runtime.Modules.StatSystem.StatModifiers
{
    public class HealthModifier : StatModifier, IDamage
    {
        public GameObject Attacker { get; set; }
        public bool IsCriticalHit { get; set; }
        public new IDamageSource Source { get; set; }
    }
}

using Code.Logic.Damage;
using UnityEngine;

namespace Code.ConfigData.StatSystem.StatModifiers
{
    public class HealthModifier : StatModifier, IDamage
    {
        public GameObject Attacker { get; set; }
        public bool IsCriticalHit { get; set; }
        public IDamageSource Source { get; set; }
    }
}

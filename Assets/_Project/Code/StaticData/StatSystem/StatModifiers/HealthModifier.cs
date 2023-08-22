using Code.Logic.Damage;
using UnityEngine;

namespace Code.StaticData.StatSystem.StatModifiers
{
    public class HealthModifier : StatModifier, IDamage
    {
        public GameObject Attacker { get; set; }
        public bool IsCriticalHit { get; set; }
        public new int Magnitude { get; set; }
        public new IDamageSource Source { get; set; }
    }
}

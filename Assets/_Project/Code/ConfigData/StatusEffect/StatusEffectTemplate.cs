using Code.ConfigData.Identifiers;
using UnityEngine;

namespace Code.ConfigData.StatusEffect
{
    public abstract class StatusEffectTemplate : ScriptableObject
    {
        public string Name;
        public string Description;
        public StatusEffectId StatusEffectId;
        public float Duration;
    }
}
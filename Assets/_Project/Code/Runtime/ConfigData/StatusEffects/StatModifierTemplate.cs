using Code.Runtime.ConfigData.StatSystem;
using UnityEngine;

namespace Code.Runtime.ConfigData.StatusEffects
{
    public abstract class StatModifierTemplate : ScriptableObject
    {
        public abstract string StatName { get; }
        public abstract ModifierOperationType Type { get; }
        
        public abstract int Magnitude { get; }
    }
}
using Code.ConfigData.StatSystem;
using UnityEngine;

namespace Code.ConfigData.StatusEffects
{
    public abstract class StatModifierTemplate : ScriptableObject
    {
        public abstract string StatName { get; }
        public abstract ModifierOperationType Type { get; }
    }
}
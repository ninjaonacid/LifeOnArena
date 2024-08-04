using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.GameplayEffects
{
    public abstract class StatModifierBlueprint : ScriptableObject
    {
        public abstract string StatName { get; }
        public abstract ModifierOperationType Type { get; }
        
        public abstract int Magnitude { get; }
    }
}
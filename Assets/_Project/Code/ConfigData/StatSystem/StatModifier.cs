using System;
using Object = UnityEngine.Object;

namespace Code.ConfigData.StatSystem
{
    public enum ModifierOperationType
    {
        Additive,
        Multiplicative,
        Override
    }
    
    public class StatModifier 
    {
        public Object Source { get; set; }
        public int Magnitude { get; set; }
        
        public ModifierOperationType OperationType;
    
    }
}

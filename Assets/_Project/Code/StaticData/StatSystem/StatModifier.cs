
using System;
using Object = UnityEngine.Object;

namespace Code.StaticData.StatSystem
{
    public enum ModifierOperationType
    {
        Additive,
        Multiplicative,
        Override
    }
    public class StatModifier 
    {
        public Object Source {get; set; }
        public int Magnitude;
        public ModifierOperationType OperationType;
    
    }
}

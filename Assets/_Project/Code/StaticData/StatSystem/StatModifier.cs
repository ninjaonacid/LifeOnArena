
using UnityEngine;

namespace Code.StaticData.StatSystem
{

    public enum ModifierOperationType
    {
        Additive,
        Multiplicative,
        Override
    }

    public class StatModifier : MonoBehaviour
    {
        public Object Source {get; set; }
        public int Magnitude;
        public ModifierOperationType OperationType;
    }
}

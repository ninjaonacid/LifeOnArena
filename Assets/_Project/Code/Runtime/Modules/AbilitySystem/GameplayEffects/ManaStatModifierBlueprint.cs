using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.GameplayEffects
{
    public class ManaStatModifierBlueprint : StatModifierBlueprint
    {
        [SerializeField] private string _statName;
        [SerializeField] private ModifierOperationType _operationType;
        [SerializeField] private int _magnitude;
        public override string StatName => _statName;
        public override ModifierOperationType Type => _operationType;
        public override int Magnitude => _magnitude;
    }
}
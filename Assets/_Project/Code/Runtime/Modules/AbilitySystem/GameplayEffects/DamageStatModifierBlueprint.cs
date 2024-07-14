using Code.Runtime.Logic.Damage;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.GameplayEffects
{
    public class DamageStatModifierBlueprint : StatModifierBlueprint
    {
        [SerializeField] private string _statName = "Health";
        [SerializeField] private ModifierOperationType _operationType;
        [SerializeField] private int _magnitude;
        [SerializeField] private DamageType _damageType;
        public override string StatName => _statName;
        public override ModifierOperationType Type => _operationType;
        public override int Magnitude => _magnitude;
        public DamageType DamageType => _damageType;
    }
}

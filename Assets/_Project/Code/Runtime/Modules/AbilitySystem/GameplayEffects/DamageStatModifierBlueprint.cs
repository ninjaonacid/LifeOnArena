using Code.Runtime.Logic.Damage;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.GameplayEffects
{
    public class DamageStatModifierBlueprint : StatModifierBlueprint
    {
        [SerializeField] private string _statName = "Health";
        [SerializeField] private ModifierOperationType _operationType;
        [SerializeField] private int _magnitude;
        [Range(0, 3)]
        [SerializeField] private float _scaleFactor;
        [SerializeField] private DamageType _damageType;
        public override string StatName => _statName;
        public override ModifierOperationType Type => _operationType;
        public override int Magnitude => _magnitude;
        public float ScaleFactor => _scaleFactor;
        public DamageType DamageType => _damageType;
    }
}

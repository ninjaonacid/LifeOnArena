using Code.ConfigData.StatSystem;
using UnityEngine;

namespace Code.ConfigData.StatusEffects
{
    [CreateAssetMenu(menuName = "Config/StatModifiers/DamageModifier", fileName = "DamageModifier")]
    public class DamageStatModifierTemplate : StatModifierTemplate
    {
        [SerializeField] private string _statName;
        [SerializeField] private ModifierOperationType _operationType;
        [SerializeField] private int _magnitude;
        public override string StatName => _statName;
        public override ModifierOperationType Type => _operationType;
        public override int Magnitude => _magnitude;
    }
}

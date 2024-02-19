using Code.ConfigData.StatSystem;
using UnityEngine;

namespace Code.ConfigData.StatusEffects
{
    [CreateAssetMenu(menuName = "Config/StatModifiers/DamageModifier", fileName = "DamageModifier")]
    public class DamageStatModifierTemplate : StatModifierTemplate
    {
        public override string StatName => "Health";
        public override ModifierOperationType Type => ModifierOperationType.Additive;
    }
}

using Code.ConfigData.StatSystem;

namespace Code.ConfigData.StatusEffects
{
    public class DamageStatModifierTemplate : StatModifierTemplate
    {
        public override string StatName => "Health";
        public override ModifierOperationType Type => ModifierOperationType.Additive;
    }
}

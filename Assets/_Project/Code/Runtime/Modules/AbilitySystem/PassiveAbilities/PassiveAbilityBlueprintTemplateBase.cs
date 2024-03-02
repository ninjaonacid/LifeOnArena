using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.PassiveAbilities
{
    public abstract class PassiveAbilityBlueprintTemplateBase : AbilityBlueprintBase
    {
        public string Description;
        public Sprite RarityIcon;
        public abstract IPassiveAbility GetAbility();
    }
}

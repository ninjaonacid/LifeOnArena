using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.PassiveAbilities
{
    public abstract class PassiveAbilityBlueprintTemplateBase : AbilityBlueprintBase
    {
        public Sprite RarityIcon;
        public abstract IPassiveAbility GetAbility();
    }
}

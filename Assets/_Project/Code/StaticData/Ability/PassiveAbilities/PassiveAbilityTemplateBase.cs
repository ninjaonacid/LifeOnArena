using UnityEngine;

namespace Code.StaticData.Ability.PassiveAbilities
{
    public abstract class PassiveAbilityTemplateBase : AbilityBase
    {
        public string Description;

        public abstract IPassiveAbility GetAbility();
    }
}

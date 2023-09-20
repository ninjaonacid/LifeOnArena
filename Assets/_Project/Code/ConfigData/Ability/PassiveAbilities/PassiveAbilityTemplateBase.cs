using UnityEngine;

namespace Code.ConfigData.Ability.PassiveAbilities
{
    public abstract class PassiveAbilityTemplateBase : AbilityBase
    {
        public string Description;
        public Sprite RarityIcon;
        public abstract IPassiveAbility GetAbility();
    }
}

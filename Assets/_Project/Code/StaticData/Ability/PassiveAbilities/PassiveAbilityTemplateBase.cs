using UnityEngine;

namespace Code.StaticData.Ability.PassiveAbilities
{
    public abstract class PassiveAbilityTemplateBase : AbilityBase
    {
        public string Description;
        public Sprite RarityIcon;
        public abstract IPassiveAbility GetAbility();
    }
}

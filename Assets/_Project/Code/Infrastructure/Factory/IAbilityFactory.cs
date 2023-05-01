
using Code.Services;
using Code.StaticData.Ability;
using Code.StaticData.Ability.PassiveAbilities;

namespace Code.Infrastructure.Factory
{
    public interface IAbilityFactory : IService
    {
        AbilityTemplateBase CreateAbilityTemplate(string heroAbilityId);
        PassiveAbilityTemplateBase GetRandomPassiveAbility();
        PassiveAbilityTemplateBase CreatePassive(string abilityId);
    }
}
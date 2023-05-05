using Code.Services;
using Code.StaticData.Ability;
using Code.StaticData.Ability.PassiveAbilities;

namespace Code.Infrastructure.Factory
{
    public interface IAbilityFactory : IService
    {
        AbilityTemplateBase CreateAbilityTemplate(int heroAbilityId);
        PassiveAbilityTemplateBase CreatePassive(string abilityId);
    }
}
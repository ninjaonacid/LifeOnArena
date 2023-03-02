
using Code.Services;
using Code.StaticData.Ability;
using Code.StaticData.Ability.PassiveAbilities;

namespace Code.Infrastructure.Factory
{
    public interface IAbilityFactory : IService
    {
        AbilityTemplateBase CreateAbility(string heroAbilityId);
        PassiveAbilityTemplateBase GetRandomPassiveAbility();
    }
}
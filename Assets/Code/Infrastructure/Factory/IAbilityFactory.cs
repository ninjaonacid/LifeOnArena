
using Code.Services;
using Code.StaticData.Ability;

namespace Code.Infrastructure.Factory
{
    public interface IAbilityFactory : IService
    {
        AbilityTemplateBase CreateAbility(string heroAbilityId);
    }
}